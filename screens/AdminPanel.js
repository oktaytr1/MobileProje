import React from 'react';
import { View, StyleSheet, ScrollView } from 'react-native';
import { Text, Button, Avatar } from 'react-native-paper';

// AdminPanel bileşeni
const AdminPanel = ({ navigation }) => {
  // Navigasyon için butonların etiket ve yönlendirme rotalarını içeren dizi
  const buttons = [
    { label: 'Hasta Ekle', route: 'HastaEkle' }, // Hasta ekleme ekranına yönlendirme
    { label: 'Tahlil Ekle', route: 'RaporEkle' }, // Tahlil ekleme ekranına yönlendirme
    { label: 'Tahlil Listele', route: 'RaporListe' }, // Tahlilleri listeleme ekranına yönlendirme
    
    { label: 'Kılavuz Ekle', route: 'KilavuzEkle' }, // Kılavuz ekleme ekranına yönlendirme
  ];

  return (
    // Ekranı kaydırılabilir hale getirmek için ScrollView kullanımı
    <ScrollView contentContainerStyle={styles.container}>
      {/* Avatar ikonu, paneli temsil ediyor */}
      <Avatar.Icon 
        size={120} 
        icon="account-cog" 
        style={styles.avatar} 
      />

      {/* Admin panel başlığı */}
      <Text style={styles.title}>Admin Panel</Text>

      {/* Butonları oluşturmak için buttons dizisi üzerinde döngü */}
      {buttons.map((button, index) => (
        <Button
          key={index} // Her butona benzersiz bir anahtar atama
          mode="contained" // Dolgu stilinde buton
          onPress={() => navigation.navigate(button.route)} // İlgili ekrana yönlendirme
          style={styles.button}
          contentStyle={styles.buttonContent}
          labelStyle={styles.buttonText}
        >
          {button.label} {/* Buton metni */}
        </Button>
      ))}
    </ScrollView>
  );
};

// Stiller aşağıda tanımlanmıştır
const styles = StyleSheet.create({
  container: {
    flexGrow: 1,
    justifyContent: 'flex-start',
    alignItems: 'center',
    padding: 25,
    backgroundColor: '#E3F2FD',
  },
  avatar: {
    backgroundColor: '#FF5722',
    marginBottom: 30,
    borderWidth: 5,
    borderColor: '#FFFFFF',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 3 },
    shadowOpacity: 0.2,
    shadowRadius: 5,
    elevation: 5,
  },
  title: {
    fontSize: 32,
    fontWeight: '700',
    marginBottom: 50,
    color: '#212121',
    fontFamily: 'Roboto',
  },
  button: {
    width: '100%',
    marginBottom: 20,
    backgroundColor: '#0288D1',
    borderRadius: 12,
    paddingVertical: 15,
    elevation: 3,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.1,
    shadowRadius: 3,
  },
  buttonContent: {
    height: 50,
  },
  buttonText: {
    fontSize: 18,
    fontWeight: '600',
    color: '#FFFFFF',
    fontFamily: 'Roboto',
  },
});

export default AdminPanel;
