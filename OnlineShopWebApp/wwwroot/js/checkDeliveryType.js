function checkDeliveryType() {
    var radioButton = document.getElementById('pickUpRadioButton');
    var hiddenBlock = document.getElementById('hiddenBlock');
    hiddenBlock.hidden = radioButton.checked;
}