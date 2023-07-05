import React from 'react';
import { StyleSheet , View, Image, Text, TouchableWithoutFeedback } from 'react-native';
import { Animated, Easing } from 'react-native';
import 'u/MyConstants';

let gridCellSize = (
    SCREEN__WIDTH*0.8 -
    SETTINGS__PADDING*2 -
    SETTINGS__PADDING*2/3 -
    SETTINGS__GRIDMARGIN*3*2

)/3;

const styles = StyleSheet.create({
    cellWrap: {
        height: gridCellSize*0.65,
        width: gridCellSize,
        borderRadius: 4,
        justifyContent: 'center',
        alignItems: 'center',
        margin: SETTINGS__GRIDMARGIN,
        backgroundColor: '#EEE4DA'
    },
    caseText: {
        fontFamily: 'NerisLight',
        fontSize: 20,
    }
});


export default class GridCell extends React.Component {
    constructor(props) {
        super(props);
    }

    shouldComponentUpdate(nextProps) {
         const { selected } = nextProps;
         const { selected: oldProp } = this.props;

         let selectedChanged = selected !== oldProp;

         return selectedChanged;
    }

    render() {
        const {  textUnit, selected, GridCellClick  } = this.props;

        return (
            <TouchableWithoutFeedback onPress={ () => GridCellClick() }>
                <View style={[
                    styles.cellWrap,
                    {backgroundColor: textUnit%2 == 1 ? '#EEE4DA' : '#EDE0C8'},
                    {borderWidth: selected ? 1 : 0 }]}>
                    <Text style={styles.caseText}> { textUnit } x {textUnit} </Text>
                </View>
            </TouchableWithoutFeedback>
    )}
}
