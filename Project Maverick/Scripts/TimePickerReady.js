
    $(function () {
        $("input[type='time']")
            .timepicker({
                'show2400': true,
                'timeFormat': 'H:i',
                'scrollDefault': 'now',
                'step': function (i) {
                    return (i % 2) ? 15 : 45
                }

            });
    })
