/* -------------------------------------------------------------------------
 * CUSTOM JQUERY FUNCTIONS AND WRAPPERS
 * DATE: 6-12-2018
 * AUTHOR: Various sources
 * ------------------------------------------------------------------------
 */

function MessageBoxConfirm(message, title) {
    return $.MessageBox({
        buttonDone: "Ναι",
        buttonFail: "Όχι",
        speed: 250,
        top: "35%",
        width: "auto",
        queue: false,
        title: title,
        message: message
    });
}

function MessageBoxAlert(message, title) {
    return $.MessageBox({
        speed: 250,
        top: "40%",
        title: title,
        width: "auto",
        queue: false,
        message: message
    });
}

function TryParseInt(str, defaultValue) {
    var retValue = defaultValue;
    if (str !== null) {
        if (str.length > 0) {
            if (!isNaN(str)) {
                retValue = parseInt(str);
            }
        }
    }
    return retValue;
}
