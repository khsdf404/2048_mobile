import React from 'react';
import { StyleSheet , View, Image, Text, Pressable } from 'react-native';

import 'u/MyConstants';

const styles = StyleSheet.create({
     progressBarWrap: {
         backgroundColor: '#00000000', // не трогай блять
         width: (SCREEN__WIDTH*0.8 - SETTINGS__PADDING*2),
         height: 22,
         justifyContent: 'flex-start',
         alignItems: 'flex-start',
         padding: 4,
         borderWidth: 1,
         borderRadius: 4,
         borderTopWidth: 0,
         borderTopLeftRadius: 0,
         borderTopRightRadius: 0,
     },
     animText: {
         justifyContent: 'space-between',
         alignItems: 'center',
         marginTop: SETTINGS__MT,
         padding: 6,
         flexDirection: 'row',

         borderWidth: 1,
         borderBottomWidth: 0,
         borderRadius: 4,
         borderBottomLeftRadius: 0,
         borderBottomRightRadius: 0,
     }
});

let fullWidth = SETTINGS__WIDTH - SETTINGS__PADDING;
export default class ProgressBar extends React.Component {
    constructor(props) {
        super(props);
    }
    state = { currProcent: animSpeed/1000 }

    progressBarTouch(e) {
        if  ( e.nativeEvent.locationX <= fullWidth  && e.nativeEvent.pageX > (SCREEN__WIDTH - SETTINGS__WIDTH)) {
            this.setState({currProcent: e.nativeEvent.locationX/(fullWidth)}, () => {
                let x = this.state.currProcent;
                animSpeed = Math.floor(976.6615*x*x-2006.9923*x+1059.3308);
                // x: 1, 0.5, 0.03,
                // y: 29 300 1000
            });
        }
    }

    render() {
        return (
        <View>
            <View style={styles.animText}>
                <Text style={[
                    {fontSize: 16},
                    {fontFamily: 'NerisBold'},
                    {marginLeft: 0.15*(SETTINGS__WIDTH - 2*SETTINGS__PADDING)}
                ]}>
                    Animation speed is {animSpeed > 30
                        ? '{ '+Math.ceil(this.state.currProcent*1000)/10+' }'
                        : '{ '+0+' }'
                    }
                </Text>
            </View>
            <View
                onTouchMove={(e) => this.progressBarTouch(e)}
                style={styles.progressBarWrap}>
                <Pressable style={[
                    {backgroundColor: '#09c'},
                    {borderRadius: 2},
                    {height: 5},
                    {width: this.state.currProcent*100 + '%' }
                ]}></Pressable>
            </View>
        </View>
    )}
}
