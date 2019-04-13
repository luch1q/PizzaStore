function removeItem(ingid) {
    $.ajax({
        type: "POST",
        url: " /Lego/RemoveItem",
        data: { ingredientID: ingid },
        success: function (resultData) {
            // take the result data and update the div
            $("#list_of_ingredients").load(" #list_of_ingredients");
            $("#images_of_constructor").load(" #images_of_constructor");
        }
    });
}
function addItem(ingid) {
    $.ajax({
        type: "POST",
        url: " /Lego/AddItem",
        data: { ingredientID: ingid },
        success: function (resultData) {
            // take the result data and update the div
            $("#list_of_ingredients").load(" #list_of_ingredients");
            $("#images_of_constructor").load(" #images_of_constructor");
        }
    });
}