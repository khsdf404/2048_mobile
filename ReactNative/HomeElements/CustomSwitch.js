import React from 'react';
import { StyleSheet , View, Text, TouchableWithoutFeedback } from 'react-native';
import { Animated, Easing } from 'react-native';

import 'u/MyConstants';


export default class ProgressBar extends React.Component {
    constructor(props) {
        super(props);
    }
    state = {
        isEnabled: true
    }

     toggleSwitch() {
         this.setState({isEnabled: !this.state.isEnabled}, ()=>{
             appearenceAllowed = this.state.isEnabled; 
         }) ;
     }

//2aa11d green
    render() {
        const { isEnabled } = this.state;
        return (
            <TouchableWithoutFeedback onPress={() => this.toggleSwitch()}>
                <View style={[
                    {backgroundColor: isEnabled ? '#2aa11d':'#000'},
                    {width: 32}, {height: 14},
                    {marginTop: 4},
                    {paddingVertical: 8 },
                    {paddingHorizontal: 1 },
                    {borderRadius: 45 },
                    {justifyContent: 'center'},
                ]}>
                    <View style={[
                        {backgroundColor: '#fff'},
                        {width: 14}, {height: 14},
                        {margin: isEnabled ? 16 : 0 },
                        {borderRadius: 45 },
                        {justifyContent: 'flex-start'},
                        {alignItems: 'flex-start'}
                    ]}>
                    </View>
                </View>
            </TouchableWithoutFeedback>
    )}
}
