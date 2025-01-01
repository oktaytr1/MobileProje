import React, { useState } from 'react';
import { ScrollView, View, StyleSheet, Alert } from 'react-native';
import { Text, TextInput, Button, Avatar, Provider } from 'react-native-paper';
import { doc, setDoc, getDoc } from 'firebase/firestore';
import { firestore } from '../firebase';
import DateTimePicker from '@react-native-community/datetimepicker';

const PatientRegistration = () => {
  const [formData, setFormData] = useState({
    patientNumber: '',
    firstName: '',
    lastName: '',
    email: '',
    birthDate: null,
  });
  const [datePickerVisible, setDatePickerVisible] = useState(false);

  const handleInputChange = (field, value) => {
    setFormData({ ...formData, [field]: value });
  };

  const calculateAgeInMonths = (dateOfBirth) => {
    const today = new Date();
    const birthDate = new Date(dateOfBirth);

    const yearDiff = today.getFullYear() - birthDate.getFullYear();
    const monthDiff = today.getMonth() - birthDate.getMonth();

    return yearDiff * 12 + monthDiff;
  };

  const savePatientData = async () => {
    const { patientNumber, firstName, lastName, email, birthDate } = formData;

    if (!patientNumber || !firstName || !lastName || !email || !birthDate) {
      Alert.alert('Error', 'Please fill in all fields.');
      return;
    }

    try {
      const patientRef = doc(firestore, 'patients', patientNumber);
      const existingPatient = await getDoc(patientRef);

      if (existingPatient.exists()) {
        Alert.alert('Error', 'This patient number is already registered.');
        return;
      }

      const ageInMonths = calculateAgeInMonths(birthDate);

      await setDoc(patientRef, {
        patientNumber,
        firstName,
        lastName,
        email,
        birthDate: birthDate.toISOString(),
        ageInMonths,
      });

      Alert.alert('Success', 'Patient data saved successfully.');
      setFormData({ patientNumber: '', firstName: '', lastName: '', email: '', birthDate: null });
    } catch (error) {
      console.error('Error:', error.message);
      Alert.alert('Error', 'An error occurred while saving patient data: ' + error.message);
    }
  };

  return (
    <Provider>
      <ScrollView contentContainerStyle={styles.scrollContainer}>
        <View style={styles.iconWrapper}>
          <Avatar.Icon size={100} icon="account-plus" style={styles.avatarStyle} />
        </View>
        <Text style={styles.headerText}>Register Patient</Text>
        <TextInput
          mode="outlined"
          label="Patient Number"
          value={formData.patientNumber}
          onChangeText={(text) => handleInputChange('patientNumber', text)}
          style={styles.inputField}
          keyboardType="numeric"
        />
        <TextInput
          mode="outlined"
          label="First Name"
          value={formData.firstName}
          onChangeText={(text) => handleInputChange('firstName', text)}
          style={styles.inputField}
        />
        <TextInput
          mode="outlined"
          label="Last Name"
          value={formData.lastName}
          onChangeText={(text) => handleInputChange('lastName', text)}
          style={styles.inputField}
        />
        <TextInput
          mode="outlined"
          label="Email"
          value={formData.email}
          onChangeText={(text) => handleInputChange('email', text)}
          style={styles.inputField}
          keyboardType="email-address"
        />
        <Button
          mode="outlined"
          onPress={() => setDatePickerVisible(true)}
          style={[styles.inputField, styles.dateButton]}
          labelStyle={styles.dateButtonText}
        >
          {formData.birthDate ? formData.birthDate.toDateString() : 'Select Birth Date'}
        </Button>
        {datePickerVisible && (
          <DateTimePicker
            value={formData.birthDate || new Date()}
            mode="date"
            display="default"
            onChange={(event, date) => {
              setDatePickerVisible(false);
              if (date) handleInputChange('birthDate', date);
            }}
          />
        )}
        <Button
          mode="contained"
          onPress={savePatientData}
          style={styles.saveButton}
        >
          Save Patient
        </Button>
      </ScrollView>
    </Provider>
  );
};

const styles = StyleSheet.create({
  scrollContainer: {
    flexGrow: 1,
    padding: 20,
    backgroundColor: '#E3F2FD',
    justifyContent: 'center',
  },
  iconWrapper: {
    justifyContent: 'center',
    alignItems: 'center',
  },
  avatarStyle: {
    backgroundColor: '#00838F',
    marginBottom: 20,
  },
  headerText: {
    fontSize: 22,
    fontWeight: 'bold',
    marginBottom: 20,
    textAlign: 'center',
    color: '#006064',
  },
  inputField: {
    marginBottom: 15,
  },
  dateButton: {
    borderColor: '#004D40',
    borderWidth: 1,
    backgroundColor: '#B2DFDB',
  },
  dateButtonText: {
    color: '#004D40',
  },
  saveButton: {
    marginTop: 20,
    backgroundColor: '#0288D1',
  },
});

export default PatientRegistration;