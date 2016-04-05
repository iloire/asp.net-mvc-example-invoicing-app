/*
 * Localized default methods for the jQuery validation plugin.
 * Locale: ES
 */
jQuery.extend(jQuery.validator.methods, {
    date: function (value, element) {
        return this.optional(element) || /^\d\d?\.\d\d?\.\d\d\d?\d?$/.test(value);
    },
    number: function (value, element) {
        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:\.\d{3})+)(?:,\d+)?$/.test(value);
    },
    range: function (value, element, param) {
        var val1 = parseInt(String(param[0]).replace(',', '.'), 10);
        var val2 = parseInt(String(param[1]).replace(',', '.'),10);
        var val = parseInt(String(value).replace(',', '.'),10);

        return this.optional(element) || (val >= val1 && val <= val2);
    }
});