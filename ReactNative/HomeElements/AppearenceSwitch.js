import React from 'react';
import { StyleSheet , View, Text } from 'react-native';
import { Animated, Easing } from 'react-native';

import 'u/MyConstants';

import CustomSwitch from 'u/HomeElements/CustomSwitch'


const styles = StyleSheet.create({
     switchWrap: {
         backgroundColor: '#00000000', // не трогай блять
         width: (SCREEN__WIDTH*0.8 - SETTINGS__PADDING*2),
         marginTop: SETTINGS__MT,
         justifyContent: 'space-between',
         alignItems: 'center',
         flexDirection: 'row',
         borderWidth: 1,
         borderRadius: 4,
         paddingHorizontal: SETTINGS__PADDING*1.7,
         paddingVertical: SETTINGS__PADDING*0.6
     }
});


export default class ProgressBar extends React.Component {
    constructor(props) {
        super(props);
    }
    state = { isEnabled: appearenceAllowed }

     toggleSwitch() {
         this.setState({isEnabled: !this.state.isEnabled});
     }

//
    render() {
        return (
            <View style={styles.switchWrap}>
                <Text style={[{fontFamily: 'NerisBold'}]}> Toggle appearence animation</Text>
                <CustomSwitch />
            </View>
    )}
}
