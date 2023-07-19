function FillSelect(url, filter, selectId) {
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: url,
        data: filter,
        success: function (data) {
            $.each(data, function (i, item) {
                $(selectId).append($('<option>', {
                    value: item.value,
                    text: item.text,
                    selected: item.selected,
                }));
            });
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}