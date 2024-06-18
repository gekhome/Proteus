$.extend({
    alert: function (message, title) {
        $("<div></div>").dialog({
            buttons: { "Ok": function () { $(this).dialog("close"); } },
            close: function (event, ui) { $(this).remove(); },
            resizable: false,
            title: title,
            modal: false
        }).text(message);
    }
});

$(function () {
    $.getScript('http://code.jquery.com/jquery-1.8.3.js');
    $.getScript('http://code.jquery.com/ui/1.10.0/jquery-ui.js');
    window.alert = function (message) {
        $.getScript('http://code.jquery.com/jquery-1.8.3.js');
        $.getScript('http://code.jquery.com/ui/1.10.0/jquery-ui.js');
        var overlay = $('', { id: 'overlay' });

        $('').html(message).dialog({
            modal: false,
            resizable: false,
            title: 'Alert!'
        });
    };
});
