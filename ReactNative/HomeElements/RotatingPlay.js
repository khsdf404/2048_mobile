import React, {useState} from 'react';
import { Easing, StyleSheet , View, Image, Button, TouchableWithoutFeedback, Animated  } from 'react-native';

import { Dimensions } from 'react-native'
var screenWidth = Dimensions.get('window').width;
var screenHeight = Dimensions.get('window').height;


const styles = StyleSheet.create({
    btnWrapp2: {
        alignItems: 'center',
        justifyContent: 'center',
        width: screenWidth,
        height: 200
    },
    playTriangle : {
        position: 'absolute',
        width: 100,
        height: 100
    },
    playOuterShell : {
        position: 'absolute',
        width: 140,
        height: 100
    },
    playInnerShell : {
        position: 'absolute',
        width: 100,
        height: 100
    },
});


export default class RotatingPlay extends React.Component {
    constructor() {
        super()
        this.innerDegree = new Animated.Value(0);
        this.outerDegree = new Animated.Value(60);
    }
    animateTo(value, direction, speed) {
        Animated.timing(value, {
            toValue: 180*direction,
            duration: speed*150,
            useNativeDriver: false,
            easing: Easing.linear
        }).start(({ finished }) => {
            value.setValue(0);
            this.animateTo(value, direction, speed);
        });
    }

    shouldComponentUpdate(nextProps){
        return false;
    }


    render() {
        // var outerDegree = this.outerDegree.interpolate({ inputRange: [0, 180], outputRange: ["0deg", "360deg"] })
        // var innerDegree = this.innerDegree.interpolate({ inputRange: [0, 180], outputRange: ["0deg", "360deg"] })
        // return (
        //     <Animated.View style={styles.btnWrapp2}>
        //         <Animated.Image onPress={this.animateTo(this.outerDegree, -1, 400)} style={[styles.playOuterShell, { transform: [ {scale:2.1}, { rotate: outerDegree }] }]} source={require('u/assets/playCircle.png')}/>
        //         <Animated.Image onPress={this.animateTo(this.innerDegree, 1, 60)} style={[styles.playInnerShell, { transform: [ {scale:1.8}, { rotate: innerDegree }] }]} source={require('u/assets/playCircle.png')}/>
        //         <Animated.Image style={[styles.playTriangle, { transform: [{ scale: 2.2 }] }]} source={require('u/assets/playTriangle.png')}/>
        //     </Animated.View>
        // )
        return (
            <View style={styles.btnWrapp2}>
                <Image style={[styles.playOuterShell, { transform: [ {scale:2.1}, { rotate: '0deg' }] }]} source={require('u/assets/polina_play.png')}/>

            </View>
        )
    }
}
