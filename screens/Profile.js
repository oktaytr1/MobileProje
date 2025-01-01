import React, { useState, useEffect } from 'react';
import { View, StyleSheet, Alert, TextInput, ScrollView } from 'react-native';
import { Text, Button } from 'react-native-paper';
import { auth, firestore } from '../firebase';
import { doc, getDoc, collection, query, where, getDocs, updateDoc } from 'firebase/firestore';

const Profile = () => {
  // Kullanıcı form verilerini saklamak için state
  const [formData, setFormData] = useState({
    patientNumber: '',
    firstName: '',
    lastName: '',
    email: '',
    age: ''
  });

  // Yükleme durumunu kontrol etmek için state
  const [loading, setLoading] = useState(false);

  // Bileşen ilk yüklendiğinde profil bilgilerini yükleme
  useEffect(() => {
    loadProfile();
  }, []);

  // Profil bilgilerini Firebase'den yükleme fonksiyonu
  const loadProfile = async () => {
    setLoading(true);
    try {
      // Oturum açmış kullanıcının e-posta adresini alma
      const userEmail = auth.currentUser?.email;
      if (!userEmail) {
        showError('Oturum açmış bir kullanıcı bulunamadı.');
        return;
      }

      // Firestore'da "patients" koleksiyonunda e-posta adresine göre sorgu oluşturma
      const patientsRef = collection(firestore, 'patients');
      const q = query(patientsRef, where('email', '==', userEmail));
      const querySnapshot = await getDocs(q);

      // Eğer sorgudan sonuç çıkmazsa hata mesajı göster
      if (querySnapshot.empty) {
        showError('E-posta adresinizle eşleşen bir hasta bulunamadı.');
        return;
      }

      // Kullanıcı verilerini state'e kaydetme
      const patientData = querySnapshot.docs[0].data();
      setFormData({
        patientNumber: patientData.patientNumber || '',
        firstName: patientData.firstName || '',
        lastName: patientData.lastName || '',
        email: patientData.email || '',
        age: patientData.age || ''
      });
    } catch (error) {
      showError('Profil verileri yüklenirken bir sorun oluştu: ' + error.message);
    } finally {
      setLoading(false);
    }
  };

  // Hata mesajlarını göstermek için bir yardımcı fonksiyon
  const showError = (message) => {
    Alert.alert('Hata', message);
    setLoading(false);
  };

  // Profil bilgilerini kaydetme fonksiyonu
  const handleSave = async () => {
    const { firstName, lastName } = formData;

    // Ad ve soyad alanlarının boş olmadığını kontrol et
    if (!firstName || !lastName) {
      showError('Ad ve soyad alanları boş bırakılamaz.');
      return;
    }

    setLoading(true);
    try {
      // Firestore'daki ilgili dokümanı güncelleme
      const patientDocRef = doc(firestore, 'patients', formData.patientNumber);
      await updateDoc(patientDocRef, {
        firstName,
        lastName,
      });

      // Başarılı güncelleme mesajı göster
      Alert.alert('Başarılı', 'Profil bilgileri güncellendi.');
    } catch (error) {
      showError('Profil güncellenirken bir sorun oluştu: ' + error.message);
    } finally {
      setLoading(false);
    }
  };

  // Formdaki alanlarda değişiklik olduğunda state'i güncelleme fonksiyonu
  const handleChange = (field, value) => {
    setFormData(prevData => ({
      ...prevData,
      [field]: value
    }));
  };

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <Text style={styles.title}>Profil Yönetimi</Text>

      {/* Hasta Numarası */}
      <View style={styles.inputContainer}>
        <Text style={styles.inputLabel}>Hasta Numarası</Text>
        <TextInput
          style={styles.input}
          value={formData.patientNumber}
          editable={false} // Düzenlenemez alan
          placeholder="Hasta Numarası"
        />
      </View>

      {/* Yaş */}
      <View style={styles.inputContainer}>
        <Text style={styles.inputLabel}>Yaş</Text>
        <TextInput
          style={styles.input}
          value={formData.age}
          editable={false} // Düzenlenemez alan
          placeholder="Yaş"
        />
      </View>

      {/* E-posta */}
      <View style={styles.inputContainer}>
        <Text style={styles.inputLabel}>E-posta</Text>
        <TextInput
          style={styles.input}
          value={formData.email}
          editable={false} // Düzenlenemez alan
          placeholder="E-posta"
        />
      </View>

      {/* Ad */}
      <View style={styles.inputContainer}>
        <Text style={styles.inputLabel}>Ad</Text>
        <TextInput
          style={styles.input}
          value={formData.firstName}
          onChangeText={(value) => handleChange('firstName', value)}
          placeholder="Ad"
        />
      </View>

      {/* Soyad */}
      <View style={styles.inputContainer}>
        <Text style={styles.inputLabel}>Soyad</Text>
        <TextInput
          style={styles.input}
          value={formData.lastName}
          onChangeText={(value) => handleChange('lastName', value)}
          placeholder="Soyad"
        />
      </View>

      {/* Kaydet Butonu */}
      <Button
        mode="contained"
        onPress={handleSave}
        loading={loading}
        disabled={loading}
        style={styles.button}
        contentStyle={styles.buttonContent}
        labelStyle={styles.buttonText}
      >
        Kaydet
      </Button>
    </ScrollView>
  );
};

// Bileşen için stiller
const styles = StyleSheet.create({
  container: {
    flexGrow: 1,
    padding: 25,
    backgroundColor: '#E3F2FD',
  },
  title: {
    fontSize: 28,
    fontWeight: 'bold',
    color: '#1B5E20',
    textAlign: 'center',
    marginBottom: 20,
  },
  inputContainer: {
    marginBottom: 20,
  },
  inputLabel: {
    fontSize: 16,
    color: '#388E3C',
    marginBottom: 5,
  },
  input: {
    height: 50,
    backgroundColor: '#fff',
    borderRadius: 10,
    paddingHorizontal: 15,
    fontSize: 16,
    color: '#333',
    borderWidth: 1,
    borderColor: '#BDBDBD',
  },
  button: {
    backgroundColor: '#0288D1',
    marginTop: 20,
    borderRadius: 10,
  },
  buttonContent: {
    height: 50,
  },
  buttonText: {
    fontSize: 18,
    fontWeight: 'bold',
    color: '#fff',
  },
});

export default Profile;
