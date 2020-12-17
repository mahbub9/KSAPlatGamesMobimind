
$(document).ready(function () {
    var searchUrl = '/game/search';

    $("#searchInput").autocomplete({
        source: function( request, response ) {
            $.ajax({
                dataType: "json", type: 'GET', url: searchUrl, data: {data:request.term},
                success: function (data) {
                    if(!data.data.length){
                        response([{label:'No matches found','value':null}])
                    }else {
                        response($.map(data.data, function (item) {
                            return {
                                label: '<li><a href="/playGame/'+ item.slug+'">'+
                                '<img src="'+item.thumbnail_image+'" width="50" height="50"/></a>'+
                                '<p>'+item.name+'</p>'+
                                '</li>',
                                value: item.slug
                            };
                            return ;

                        }));
                    }
                },
                error: function (data) {
                    $('input.suggest-user').removeClass('ui-autocomplete-loading');
                }
            });
        },
        minLength: 1,
        select: function(event, ui) {
            window.location.href = '/playGame/'+ ui.item.value;
            /*$("#searchInput").val(ui.item.value);*/
        }
    }).data("ui-autocomplete")._renderItem = function( ul, item ) {
        return $( "<li class='ui-autocomplete-row'></li>" )
            .data( "item.autocomplete", item )
            .append( item.label )
            .appendTo( ul );
    };
});



