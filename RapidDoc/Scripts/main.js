﻿function calendar_init(url_json_calendar) {
    var date = new Date();
    var workScheduleId = $('#WorkScheduleId').val();

    $('#calendar').fullCalendar({

        dayClick: function (date, allDay, jsEvent, view) {
            var status;
            $.ajax({
                type: 'POST',
                data: JSON.stringify({ date: date }),
                contentType: 'application/json',
                success: function (data) {
                    status = 'true';
                },
                error: function () {
                    status = 'false';
                }
            });
            if (status != 'false')
                $(this).toggleClass("calendarSelected");
        },

        firstDay: 1,

        dayRender: function (date, cell) {
            $.getJSON(url_json_calendar, { 'id': workScheduleId, 'day': date.getDate(), 'month': date.getMonth()+1, 'year': date.getFullYear() },
            function (data) {

                if (data.dayOff == 'true') {
                    cell.toggleClass("calendarSelected");
                }
            });
        }
    });
}

function selectpicker_init() {
    $('.selectpicker').selectpicker({
        width: '30%'
    });
}

function duallist_init(placeholder) {
    $('.duallist').bootstrapDualListbox({
        infoText: false,
        filterPlaceHolder: placeholder
    });

    $("#duallistform").submit(function (e) {
        var duallistdata = $('.duallist').val();

        e.preventDefault();
        $.ajax({
            type: 'POST',
            data: JSON.stringify({ listdata: duallistdata, isAjax: true }),
            contentType: 'application/json',
            success: function (data) {
                if (data.result == 'Redirect') {
                    window.location = data.url;
                }
            }
        });
    });
}

function datepicker_init(lang) {
    var datepicker = $('.datepicker').datepicker({
        format: 'dd.mm.yyyy',
        autoclose: true,
        language: lang,
        todayBtn: true,
        todayHighlight: true
    });
}

function timepicker_init() {
    $('.timepicker').timepicker({
        showMeridian: false
    });
}

function datetimepicker_init(lang) {
    $('.datetimepicker').datetimepicker({
        language: lang,
        showToday: true
    });
}

function qrcode_init() {
    var qrcode = new QRCode(document.getElementById("qrcode"), {
        width: 100,
        height: 100,
        colorDark: "#27ad60",
        colorLight: "#ffffff"
    });

    qrcode.makeCode(document.location.href+'?isAfterView=true');
}

function summernote_init(lang) {
    if ((lang == "") || (lang == ""))
    {
        lang = 'en-US';
    }

    if ($(".summernote")[0]) {
        $('.summernote').summernote({
            height: 300,
            focus: true,
            lang: lang,

            toolbar: [
                ['style', ['style']], // no style button
                ['style', ['bold', 'italic', 'underline', 'clear']],
                ['fontsize', ['fontsize']],
                //['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['height', ['height']],
                //['insert', ['picture', 'link']], // no insert buttons
                ['table', ['table']], // no table button
                //['help', ['help']] //no help button
            ]
        });
    }
}

function grid_init(url_json) {
    var self = this;
    this.currentPage = 1;

    var pageLink = $(".grid-ajax-pager");
    var gridTableBody = pageLink.closest(".grid-wrap").find("tbody");

    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height()) {
            $('div#loading').html('<img src="/Content/Custom/image-icon/autoload.gif"/>');
            self.loadNextPage();
        }
    });

    this.loadNextPage = function () {
        self.currentPage++;

        $.get(url_json + self.pad(location.search) + self.currentPage)
            .done(function (response) {

                gridTableBody.append(response.Html);
                if (!response.HasItems)
                    pageLink.hide();

                $('div#loading').empty();

                $('.popover-link').popover({
                    trigger: 'hover'
                });
            })
            .fail(function () {
            });
    };

    this.pad = function (query) {
        if (query.length == 0) return "?page=";
        return query + "&page=";
    };

    $('table.grid-table > tbody > tr > td.empty:has(.text-warning)').css("background", "#f39c12");
    $('table.grid-table > tbody > tr > td.empty:has(.text-danger)').css("background", "#e74c3c");
}

function custom_tagsinputEmpl_init(url_json) {
    var element = document.getSelection('input[data-role=tagsinputEmpl]');
    if (typeof (element) != 'undefined' && element != null) {
        elt = $('input[data-role=tagsinputEmpl]');
        elt.tagsinput({
            itemValue: 'value',
            itemText: 'text',
            tagClass: function (item) {
                return 'label label-primary bts-tags';
            }
        });

        elt.tagsinput('input').typeahead({
            valueKey: 'text',
            prefetch: url_json,
            template: '<p>{{text}}</p>',
            engine: Hogan

        }).bind('typeahead:selected', $.proxy(function (obj, datum) {
            this.tagsinput('add', datum);
            this.tagsinput('input').typeahead('setQuery', '');
        }, elt));

        currentValue = $('input[data-role=tagsinputEmpl]').val();
        if (currentValue != null) {
            currentArrData = currentValue.split(",");
            $('input[data-role=tagsinputEmpl]').val('');

            if (currentArrData.length > 1) {
                for (var i = 0; i < currentArrData.length; i += 2) {
                    var key = currentArrData[i];
                    var numValue = i;
                    numValue++;
                    var value = currentArrData[numValue];
                    if(value.length > 0)
                        $('input[data-role=tagsinputEmpl]').tagsinput('add', { "value": key +"," + value, "text": value });
                }
            }
        }
    }
}

function custom_tagsinputEmpl2_init(url_json) {
    var element = document.getSelection('input[data-role=tagsinputEmpl2]');
    if (typeof (element) != 'undefined' && element != null) {
        try {
            elt2 = $('input[data-role=tagsinputEmpl2]');
            elt2.tagsinput({
                itemValue: 'value',
                itemText: 'text',
                tagClass: function (item) {
                    return 'label label-primary bts-tags';
                }
            });

            elt2.tagsinput('input').typeahead({
                valueKey: 'text',
                prefetch: url_json,
                template: '<p>{{text}}</p>',
                engine: Hogan

            }).bind('typeahead:selected', $.proxy(function (obj, datum) {
                this.tagsinput('add', datum);
                this.tagsinput('input').typeahead('setQuery', '');
            }, elt2));

            currentValue2 = $('input[data-role=tagsinputEmpl2]').val();
            if (currentValue2 != null) {
                currentArrData2 = currentValue2.split(",");
                $('input[data-role=tagsinputEmpl2]').val('');

                if (currentArrData2.length > 1) {
                    for (var i = 0; i < currentArrData2.length; i += 2) {
                        var key = currentArrData2[i];
                        var numValue = i;
                        numValue++;
                        var value = currentArrData2[numValue];
                        if (value.length > 0)
                            $('input[data-role=tagsinputEmpl2]').tagsinput('add', { "value": key + "," + value, "text": value });
                    }
                }
            }
        }
        catch (e) {

        }
    }
}
