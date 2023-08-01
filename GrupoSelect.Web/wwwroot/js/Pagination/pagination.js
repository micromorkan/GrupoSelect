function RenderPagination(paginaatual, totalregistros, qtdpage = 20) {

    var qtdpaginas = Math.ceil(totalregistros / qtdpage);

    var html = '<div class="dataTables_wrapper container-fluid dt-bootstrap4 no-footer">' +
        '<div class="row">' +
        '<div class="col-sm-12 col-md-5">';

    if (totalregistros === 0) {
        //html = html + '<div class="dataTables_info">Não há registros</div>';
        html = html + '<div class="dataTables_info"></div>';
    } else if ((paginaatual * qtdpage) >= totalregistros) {
        html = html + '<div class="dataTables_info">Exibindo ' + ((paginaatual * qtdpage) - (qtdpage - 1)) + ' a ' + totalregistros + ' de um total de ' + totalregistros + ' registros</div>';
    } else {
        html = html + '<div class="dataTables_info">Exibindo ' + ((paginaatual * qtdpage) - (qtdpage - 1)) + ' a ' + (paginaatual * qtdpage) + ' de um total de ' + totalregistros + ' registros</div>';
    }

    html = html + '</div>' +
        '<div class="col-sm-12 col-md-7">' +
        '<div class="dataTables_paginate paging_simple_numbers">' +
        '<ul class="pagination">';

    if (paginaatual === 1) {
        html = html + '<li class="paginate_button page-item previous disabled"><a href="#" class="page-link">Anterior</a></li>';
    } else {
        html = html + '<li class="paginate_button page-item previous"><a href="#" onclick="GetData(' + (paginaatual - 1) + ');" class="page-link">Anterior</a></li>';
    }

    for (var i = 1; i <= qtdpaginas; i++) {
        if (paginaatual === i) {
            html = html + '<li class="paginate_button page-item active"><a href="#" onclick="GetData(' + i + ');" class="page-link">' + i + '</a></li>';
        } else {
            html = html + '<li class="paginate_button page-item"><a href="#" onclick="GetData(' + i + ');" class="page-link">' + i + '</a></li>';
        }
    }

    if (qtdpaginas === paginaatual || qtdpaginas === 0) {
        html = html + '<li class="paginate_button page-item next disabled"><a href="#" class="page-link">Próximo</a></li>';
    } else {
        html = html + '<li class="paginate_button page-item next"><a href="#" onclick="GetData(' + (paginaatual + 1) + ');" class="page-link">Próximo</a></li>';
    }

    html = html + '</ul></div></div></div></div>';

    $('#pagination').html(html);
}

function UpdateData(pagina) {
    GetData(pagina);
}

