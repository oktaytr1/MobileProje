import React, { useState } from 'react';
import { View, StyleSheet, Alert } from 'react-native';
import { Text, TextInput, Button, Avatar } from 'react-native-paper';
import { signInWithEmailAndPassword } from 'firebase/auth';
import { auth, firestore } from '../firebase';
import { doc, getDoc } from 'firebase/firestore';

const Login = ({ navigation }) => {
  // E-posta ve şifre için state değişkenleri
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  // Sabit admin bilgileri
  const ADMIN_EMAIL = 'g201210557@gmail.com';
  const ADMIN_PASSWORD = '12345678';

  // Giriş işlemi
  const handleLogin = async () => {
    // E-posta ve şifre boş bırakılmamalı
    if (!email.trim() || !password.trim()) {
      return Alert.alert('Hata', 'Lütfen tüm alanları eksiksiz doldurun.');
    }

    // Admin giriş kontrolü
    const isAdmin = email === ADMIN_EMAIL && password === ADMIN_PASSWORD;
    if (isAdmin) {
      // Admin olarak giriş yapıldıysa admin paneline yönlendirme
      Alert.alert('Başarılı', 'Admin olarak giriş yapıldı!');
      return navigation.navigate('AdminPanelNavigator');
    }

    try {
      // Firebase Authentication ile giriş kontrolü
      const userCredential = await signInWithEmailAndPassword(auth, email, password);
      const user = userCredential.user;

      // Firestore'dan kullanıcı bilgilerini çek
      const userRef = doc(firestore, 'users', user.uid);
      const userDoc = await getDoc(userRef);

      // Kullanıcı bilgileri var mı kontrolü
      if (userDoc.exists()) {
        const { role } = userDoc.data();

        // Kullanıcı rolüne göre yönlendirme
        switch (role) {
          case 'user':
            // Eğer kullanıcı rolü 'user' ise, kullanıcı paneline yönlendirilir
            Alert.alert('Başarılı', 'Kullanıcı olarak giriş yapıldı!');
            navigation.navigate('UserPanelNavigator');
            break;
          default:
            // Geçersiz bir rol varsa hata mesajı
            Alert.alert('Hata', 'Geçersiz kullanıcı rolü.');
        }
      } else {
        // Kullanıcı Firestore'da bulunamazsa hata mesajı
        Alert.alert('Hata', 'Kullanıcı bilgileri bulunamadı.');
      }
    } catch (error) {
      // Hatalı giriş durumunda hata mesajı
      Alert.alert('Hata', 'E-posta veya şifre hatalı.');
    }
  };

  return (
    <View style={styles.container}>
      {/* Kullanıcı avatarı */}
      <Avatar.Icon size={80} icon="account" style={styles.avatar} />
      {/* Hoş geldiniz başlığı */}
      <Text style={styles.title}>Hoş Geldiniz</Text>
      
      {/* E-posta inputu */}
      <TextInput
        mode="outlined"
        label="Email"
        value={email}
        onChangeText={setEmail}
        style={styles.input}
        keyboardType="email-address"
        placeholder="E-posta adresinizi girin"
        autoCapitalize="none"
        theme={{ colors: { primary: '#004D40' } }}
      />
      
      {/* Şifre inputu */}
      <TextInput
        mode="outlined"
        label="Şifre"
        value={password}
        onChangeText={setPassword}
        style={styles.input}
        secureTextEntry
        placeholder="Şifrenizi girin"
        theme={{ colors: { primary: '#004D40' } }}
      />
      
      {/* Giriş yapma butonu */}
      <Button
        mode="contained"
        onPress={handleLogin}
        style={styles.button}
        contentStyle={{ height: 50 }}
      >
        Giriş Yap
      </Button>
    </View>
  );
};

// Stil tanımlamaları
const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#E3F2FD', // Arka plan rengi
    padding: 20,
  },
  avatar: {
    backgroundColor: '#0288D1', // Avatar rengi
    marginBottom: 20,
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 25,
    color: '#757575', // Başlık rengi
  },
  input: {
    width: '100%',
    marginBottom: 15,
    backgroundColor: '#FFFFFF', // Input arka planı
  },
  button: {
    width: '100%',
    backgroundColor: '#0288D1', // Buton rengi
  },
});

export default Login;
