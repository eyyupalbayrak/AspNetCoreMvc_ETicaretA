function AddToCart(id) {
    $.ajax({
        type: "POST",
        url: "Cart/AddCart",
        data: { id: id, quantity: 1 },
        success: function (response) {
            Swal.fire({
                position: 'bottom-end',
                icon: 'success',
                title: 'Ürün Sepete Eklendi',
                showConfirmButton: false,
                timer: 1500
            }),
                $("#cartdropdown").load("Cart/DropdownRefresh")
            $("#cartcount").load("Cart/CartCount")
        }
    })
}


document.addEventListener('DOMContentLoaded', function () {
    var sepetiOnaylaButton = document.querySelector('.btn-order');
    var standartShippingRadio = document.getElementById('standart-shipping');
    var expressShippingFee = 100; // Hızlı kargo ücreti

    sepetiOnaylaButton.addEventListener('click', function (e) {
        // Kargo seçeneğini kontrol et
        if (standartShippingRadio.checked) {
            // Standart kargo seçiliyse, toplam tutarı güncelleme
            // Ancak session'a ekstra ücreti ekleyerek kaydetme
            e.preventDefault(); // Formun submit işlemini engelle
            var totalFiyatElement = document.getElementById('toplamfiyat');
            var currentTotal = parseFloat(totalFiyatElement.textContent.replace('₺', '').replace(',', ''));
            totalFiyatElement.textContent = (currentTotal + expressShippingFee).toFixed(2) + ' ₺';

            // Session'a ekstra ücreti kaydet
            sessionStorage.setItem('expressShippingFee', expressShippingFee);

            // Sepeti Onayla fonksiyonunu çağır
            sepetiOnayla();
        }
    });

    function sepetiOnayla() {
        // İlgili sayfaya yönlendir
        window.location.href = '/Payment/Index';
    }
});









document.addEventListener('DOMContentLoaded', function () {
    var standartShippingRadio = document.getElementById('free-shipping');
    var expressShippingRadio = document.getElementById('standart-shipping');
    var totalFiyatElement = document.getElementById('toplamfiyat');
    var expressShippingFee = 100;

    function updateTotal() {
        var currentTotal = parseFloat(totalFiyatElement.textContent.replace('', '').replace(',', ''));

        if (expressShippingRadio.checked) {
            totalFiyatElement.textContent = (currentTotal + expressShippingFee).toFixed(2);
        } else {
            totalFiyatElement.textContent = (currentTotal - expressShippingFee).toFixed(2);
        }
    }

    standartShippingRadio.addEventListener('change', updateTotal);
    expressShippingRadio.addEventListener('change', updateTotal);
});


