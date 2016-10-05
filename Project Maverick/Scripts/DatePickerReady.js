

    $(function () {
        $("input[type='date']")
                    .datepicker({           
                        dateFormat: "dd.mm.yy",
                        showWeek: true,
                        currentText: 'Now',
                        autoSize: true,
                        gotoCurrent: true,
                        showAnim: 'blind',
                    })
                    .get(0)
                    .setAttribute("type", "text"); //Disables HTML5 datepicker
    })

