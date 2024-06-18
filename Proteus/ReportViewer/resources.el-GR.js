; (function (trv, $) {
"use strict";
var sr = {
controllerNotInitialized: 'Ο Controller δεν αρχικοποιήθηκε.',
noReportInstance: 'Δεν υπάρχει στιγμιότυπο έκθεσης.',
missingTemplate: 'Το πρότυπο του ReportViewer δεν βρέθηκε. Ορίσατε το Url του προτύπου στις επιλογές.',
noReport: 'Δεν υπάρχει έκθεση.',
noReportDocument: 'Δεν υπάρχει έγγραφο έκθεσης.',
invalidParameter: 'Εισάγετε μια έγκυρη τιμή.',
invalidDateTimeValue: 'Εισάγετε μια έγκυρη ημερομηνία.',
parameterIsEmpty: 'Η τιμής της παραμέτρου δεν μπορεί να είναι κενή.',
cannotValidateType: 'Δεν μπορεί να επικυρωθεί η παράμετρος τύπου {type}.',
loadingFormats: 'Φόρτωση...',
loadingReport: 'Φόρτωση έκθεσης...',
preparingDownload: 'Προετοιμασία του εγγράφου για μεταφόρτωση. Περιμένετε...',
preparingPrint: 'Προετοιμασία του εγγράφου για εκτύπωση. Περιμένετε...',
errorLoadingTemplates: 'Σφάλμα φόρτωσης των προτύπων του viewer της έκθεσης.',
loadingReportPagesInProgress: '{0} σελίδες φορτώθηκαν μέχρι στιγμής ...',
loadedReportPagesComplete: 'Ολοκληρώθηκε. Συνολικά {0} σελίδες φορτώθηκαν.',
noPageToDisplay: "Δεν υπάρχει σελίδα προς εμφάνιση.",
errorDeletingReportInstance : 'Σφάλμα διαγραφής στιγμιότυπου της έκθεσης: {0}',
errorRegisteringViewer : 'Σφάλμα εγγραφής του viewer με την υπηρεσία (service).',
noServiceClient : 'Δεν έχει οριστεί serviceClient για αυτόν τον controller.',
errorRegisteringClientInstance : 'Σφάλμα καταχώρησης στιγμιότυπου πελάτη',
errorCreatingReportInstance : 'Σφάλμα δημιουργίας στιγμιότυπου έκθεσης (Έκθεση = {0})',
errorCreatingReportDocument : 'Σφάλμα δημιουργίας εγγράφου έκθεσης (Έκθεση = {0}; Μορφή = {1})',
unableToGetReportParameters : "Αδυναμία λήψης των παραμέτρων της έκθεσης",
};
trv.sr = $.extend(trv.sr, sr);
}(window.telerikReportViewer = window.telerikReportViewer || {}, jQuery));
