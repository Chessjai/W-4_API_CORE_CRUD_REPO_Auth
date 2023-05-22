var ProdId = '';
$(document).ready(function () {
    showProducts();
    $("#btnUpdateproduct").click(function () {

        if (ProdId != '') {
            updateProduct(ProdId)
        }
        else {
            alert("No proper Product id found for update!")
        }

    });
});

function createProduct() {
    var url = "/api/Product";
    var Product = {};

    if ($('#txtName').val() === '' || $('#txtCategory').val() === '' || $('#txtColor').val() === ''
    || $('#txtUnitPrice').val() === '' || $('#txtAvailableQuantity').val() === '') {
        alert("No filed can be left blank");
    }
    else {
        Product.name = $('#txtName').val();
        Product.category = $('#txtCategory').val();
        Product.color = $('#txtColor').val();
        Product.unitPrice = $('#txtUnitPrice').val();
        Product.availableQuantity = $('#txtAvailableQuantity').val();


        if (Product) {
            $.ajax({
                url: url,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(Product),
                type: "Post",
                success: function (result) {
                    clearForm();
                    showProducts();
                },
                error: function (msg) {
                    alert(msg);
                }

            });
        }
    }
}

function showProducts() {
    var url = "/api/Product";

    $.ajax({
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "Get",
        success: function (result) {
            if (result) {
                $("#tblProductBody").html('');
                var row = '';
                for (var i = 0; i < result.length; i++) {
                    row = row
                        + "<tr>"

                        + "<td>" + result[i].name + "</td>"
                        + "<td>" + result[i].category + "</td>"
                        + "<td>" + result[i].color + "</td>"
                        + "<td>" + result[i].unitPrice + "</td>"
                        + "<td>" + result[i].availableQuantity + "</td>"
                        + "<td><button class='btn btn-primary' onClick='editProduct(" + result[i].productId + ")'>Edit</button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<button class='btn btn-danger' onClick='deleteProduct(" + result[i].productId + ")'>Delete</button></td>"
                }
                if (row != '') {
                    $("#tblProductBody").append(row);
                }
            }
        },
        error: function (msg) {
            alert(msg);
        }

    });
}


function deleteProduct(productId) {
    var url = "/api/Product/" + productId;
    $.ajax({
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "Delete",
        success: function (result) {
            clearForm();
             alert(JSON.stringify('DO you want delete this?'));
            showProducts();
        },
        error: function ( msg) {
            alert('ERROR');
        }

    });
}





function editProduct(productId) {
    var url = "/api/Product/" + productId;
    $.ajax({
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "Get",
        success: function (result) {
            if (result) {
                ProdId = result.productId;
                $('#txtName').val(result.name);
                $('#txtCategory').val(result.category);
                $('#txtColor').val(result.color);
                $('#txtUnitPrice').val(result.unitPrice);
                $('#txtAvailableQuantity').val(result.availableQuantity);

            }
            $("#btnCreateProduct").prop('disabled', true);
            $("#btnUpdateproduct").prop('disabled', false);

        },
        error: function (msg) {
            alert(msg);
        }

    });
}

function updateProduct(id) {
    var url = "/api/Product/" + id;
    var product = {};

    product.productId = id;
    product.name = $('#txtName').val();
    product.category = $('#txtCategory').val();
    product.color = $('#txtColor').val();
    product.unitPrice = $('#txtUnitPrice').val();
    product.availableQuantity = $('#txtAvailableQuantity').val();

    if (product) {
        $.ajax({
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(product),
            type: "Put",
            success: function (result) {
                clearForm();
                showProducts();
                $("#btnCreateProduct").prop('disabled', false);
                $("#btnUpdateproduct").prop('disabled', true);

            },
            error: function (msg) {
                alert(msg);
            }

        });
    }
}




function clearForm() {
    $('#txttName').val('');
    $('#txtCategory').val('');
    $('#txtColor').val('');
    $('#txtUnitPrice').val('');
    $('#txtAvailableQuantity').val('');

}