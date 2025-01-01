import React from 'react';
import { View, StyleSheet } from 'react-native';
import { Button, Text, Avatar } from 'react-native-paper';

const HomeScreen = ({ navigation }) => {
  return (
    <View style={styles.screenContainer}>
      <Text style={styles.title}>E-LAB</Text>
      <Avatar.Icon size={100} icon="stethoscope" style={styles.avatarStyle} />
      <Text style={styles.tagline}>Sağlığınız Elinizde</Text>
      <Button
        mode="contained"
        onPress={() => navigation.navigate('Login')}
        style={styles.primaryButton}
        contentStyle={styles.primaryButtonContent}
        labelStyle={styles.primaryButtonText}
      >
        Giriş Yap
      </Button>
      <Button
        mode="outlined"
        onPress={() => navigation.navigate('Signup')}
        style={styles.secondaryButton}
        contentStyle={styles.secondaryButtonContent}
        labelStyle={styles.secondaryButtonText}
      >
        Kayıt Ol
      </Button>
    </View>
  );
};

const styles = StyleSheet.create({
  screenContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    padding: 25,
    backgroundColor: '#E3F2FD', // Hafif mavi arka plan
  },
  avatarStyle: {
    backgroundColor: '#0288D1', // Mavi avatar arka planı
    marginBottom: 25,
  },
  title: {
    fontSize: 32,
    fontWeight: '700',
    color: '#01579B', // Koyu mavi başlık
    textAlign: 'center',
    marginBottom: 15,
  },
  tagline: {
    fontSize: 18,
    fontWeight: '500',
    color: '#757575', // Gri slogan metni
    textAlign: 'center',
    marginBottom: 35,
  },
  primaryButton: {
    marginVertical: 12,
    width: '85%',
    backgroundColor: '#0288D1', // Mavi buton
  },
  primaryButtonContent: {
    height: 48,
  },
  primaryButtonText: {
    fontSize: 17,
    fontWeight: 'bold',
    color: '#FFFFFF', // Beyaz yazı rengi
  },
  secondaryButton: {
    marginVertical: 12,
    width: '85%',
    borderColor: '#0288D1',
    borderWidth: 1.5,
  },
  secondaryButtonContent: {
    height: 48,
  },
  secondaryButtonText: {
    fontSize: 17,
    fontWeight: 'bold',
    color: '#0288D1', // Mavi yazı rengi
  },
});

export default HomeScreen;
