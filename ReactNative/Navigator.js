import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';
import { createAppContainer } from '@react-navigation/stack';

import { StyleSheet, StatusBar, View } from 'react-native';

import HomePage from 'u/pages/HomePage';
import GamePage from 'u/pages/GamePage';

const css = StyleSheet.create({
    navContainer: {
        height: 100,
        backgroundColor: '#999',
        opacity: 0
    }
});

const Stack = createStackNavigator();
export default function SetScreenNavigator() {
    return ( 
    <NavigationContainer>
        <Stack.Navigator screenOptions={{headerShown: false}}>
            <Stack.Screen name="HomeScreen" component={HomePage}/>
            <Stack.Screen name="GameScreen" component={GamePage} />
        </Stack.Navigator>
    </NavigationContainer>
    );
};
