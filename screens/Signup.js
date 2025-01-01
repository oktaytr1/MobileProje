import React, { useState } from 'react';
import { View, StyleSheet, Alert } from 'react-native';
import { Text, TextInput, Button, Avatar } from 'react-native-paper';
import { auth, firestore } from '../firebase';
import { createUserWithEmailAndPassword } from 'firebase/auth';
import { doc, setDoc } from 'firebase/firestore';

const SignupScreen = ({ navigation }) => {
  // Kullanıcı e-posta ve şifresi için state değişkenleri
  const [userEmail, setUserEmail] = useState('');
  const [userPassword, setUserPassword] = useState('');

  // Kayıt işlemini yönetir
  const handleRegistration = async () => {
    // E-posta ve şifre boş bırakılmamalı
    if (!userEmail || !userPassword) {
      Alert.alert('Hata', 'Lütfen email ve şifre alanlarını doldurun.');
      return;
    }

    try {
      // Firebase Authentication ile kullanıcı kaydı oluşturuluyor
      const userCredential = await createUserWithEmailAndPassword(auth, userEmail, userPassword);
      const newUser = userCredential.user;

      // Kullanıcı bilgilerini Firestore'a kaydediyoruz
      await setDoc(doc(firestore, 'users', newUser.uid), {
        email: newUser.email,
        role: 'user', // Varsayılan olarak 'user' rolü atanıyor
      });

      // Kayıt başarılıysa uyarı veriyoruz ve giriş ekranına yönlendiriyoruz
      Alert.alert('Başarılı', 'Kayıt başarıyla tamamlandı!');
      navigation.navigate('Login');
    } catch (err) {
      // Hata durumunda hata mesajı gösteriyoruz
      Alert.alert('Hata', err.message);
    }
  };

  return (
    <View style={styles.screenContainer}>
      {/* Avatar ikonu */}
      <Avatar.Text size={90} label="KO" style={styles.avatarStyle} />
      {/* Başlık */}
      <Text style={styles.headerText}>Hesap Oluştur</Text>
      
      {/* E-posta inputu */}
      <TextInput
        mode="flat"
        label="Email"
        value={userEmail}
        onChangeText={setUserEmail}
        style={styles.inputField}
        keyboardType="email-address"
        placeholder="örnek@domain.com"
        autoCapitalize="none"
        theme={{
          colors: {
            placeholder: '#A9A9A9',
            text: '#333333',
            primary: '#008080',
          },
        }}
      />
      
      {/* Şifre inputu */}
      <TextInput
        mode="flat"
        label="Şifre"
        value={userPassword}
        onChangeText={setUserPassword}
        style={styles.inputField}
        secureTextEntry
        placeholder="Şifrenizi giriniz"
        theme={{
          colors: {
            placeholder: '#A9A9A9',
            text: '#333333',
            primary: '#008080',
          },
        }}
      />
      
      {/* Kayıt olma butonu */}
      <Button
        mode="contained"
        onPress={handleRegistration}
        style={styles.registerButton}
        contentStyle={styles.registerButtonContent}
        labelStyle={styles.registerButtonText}
      >
        Kaydol
      </Button>

      {/* Giriş yapma butonu */}
      <Button
        mode="text"
        onPress={() => navigation.navigate('Login')}
        style={styles.loginLinkButton}
        labelStyle={styles.loginLinkText}
      >
        Hesabınız var mı? Giriş Yapın
      </Button>
    </View>
  );
};

// Stil tanımlamaları
const styles = StyleSheet.create({
  screenContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    paddingHorizontal: 20,
    backgroundColor: '#E3F2FD', // Arka plan rengi
  },
  avatarStyle: {
    backgroundColor: '#0288D1', // Avatar rengi
    marginBottom: 20,
  },
  headerText: {
    fontSize: 26,
    fontWeight: '600',
    marginBottom: 20,
    color: '#004D40', // Başlık rengi
  },
  inputField: {
    width: '100%',
    marginBottom: 15,
    backgroundColor: 'transparent', // Input arka planı
  },
  registerButton: {
    width: '100%',
    marginTop: 10,
    backgroundColor: '#0288D1', // Kayıt butonu rengi
  },
  registerButtonContent: {
    height: 50,
  },
  registerButtonText: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#FFFFFF', // Kayıt butonu yazı rengi
  },
  loginLinkButton: {
    marginTop: 15,
  },
  loginLinkText: {
    color: '#008080', // Giriş linki rengi
    fontWeight: '500',
  },
});

export default SignupScreen;
