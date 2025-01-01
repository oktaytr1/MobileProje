import React from 'react';
import { View, StyleSheet } from 'react-native';
import { Button, Text } from 'react-native-paper';

// Kullanıcı paneli bileşeni
const UserPanel = ({ navigation }) => {
  return (
    <View style={styles.panelContainer}>
      {/* Kullanıcı paneli başlığı */}
      <Text style={styles.header}>Kullanıcı Paneli</Text>
      
      {/* Geçmiş Tahliller ekranına yönlendiren buton */}
      <Button
        mode="contained"
        onPress={() => navigation.navigate('PastReports')} // 'PastReports' ekranına yönlendirme
        style={styles.primaryButton}
        contentStyle={styles.buttonContent}
        labelStyle={styles.buttonText}
      >
        Geçmiş Tahliller
      </Button>

      {/* Tahlil Ara ekranına yönlendiren buton */}
      <Button
        mode="contained"
        onPress={() => navigation.navigate('SearchReports')} // 'SearchReports' ekranına yönlendirme
        style={styles.primaryButton}
        contentStyle={styles.buttonContent}
        labelStyle={styles.buttonText}
      >
        Tahlil Ara
      </Button>

      {/* Profil Yönetimi ekranına yönlendiren buton */}
      <Button
        mode="contained"
        onPress={() => navigation.navigate('Profile')} // 'Profile' ekranına yönlendirme
        style={styles.primaryButton}
        contentStyle={styles.buttonContent}
        labelStyle={styles.buttonText}
      >
        Profil Yönetimi
      </Button>
    </View>
  );
};

// Stil tanımlamaları
const styles = StyleSheet.create({
  // Genel ekran düzeni için ana kapsayıcı
  panelContainer: {
    flex: 1, 
    justifyContent: 'center', 
    alignItems: 'center', 
    padding: 20, 
    backgroundColor: '#E3F2FD', 
  },
  // Başlık stili
  header: {
    fontSize: 30, 
    fontWeight: '700', 
    color: '#004D40', 
    marginBottom: 25, 
  },
  // Buton stili
  primaryButton: {
    marginVertical: 10, 
    backgroundColor: '#0288D1', 
    width: '85%', 
    borderRadius: 8, 
  },
  // Buton içeriği stili
  buttonContent: {
    height: 50, // Buton yüksekliği
  },
  // Buton yazı stili
  buttonText: {
    fontSize: 18, // Yazı boyutu
    fontWeight: 'bold', // Kalın yazı
    color: '#FFFFFF', // Beyaz yazı rengi
  },
});

export default UserPanel;
