function removeItem(ingid) {
    $.ajax({
        type: "POST",
        url: " /Lego/RemoveItem",
        data: { ingredientID: ingid },
        success: function () {
            location.reload();
        }
    });
}
function addItem(ingid) {
    $.ajax({
        type: "POST",
        url: " /Lego/AddItem",
        data: { ingredientID: ingid },
        success: function () {
            location.reload();
        }
    });
}
function addCustomPizzaToCart() {
    $.ajax({
        type: "POST",
        url: " /Lego/AddToCart",
        success: function () {
            location.reload();
        }
    });
}