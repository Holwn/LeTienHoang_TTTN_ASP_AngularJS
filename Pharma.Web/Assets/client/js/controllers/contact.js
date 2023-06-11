var contact = {
    init: function () {
        contact.registerEvent();
    },
    registerEvent: function () {
        contact.initMap();
    },
    initMap: function () {
        const uluru = { lat: -25.363, lng: 131.044 };
        const map = new google.maps.Map(document.getElementById("map"), {
            zoom: 4,
            center: uluru,
        });
        const contentString = $('#hidContactDetail').val();
        const infowindow = new google.maps.InfoWindow({
            content: contentString,
            ariaLabel: "Uluru",
        });
        const marker = new google.maps.Marker({
            position: uluru,
            map,
            title: "Uluru (Ayers Rock)",
        });

        marker.addListener("click", () => {
            infowindow.open({
                anchor: marker,
                map,
            });
        });
    }


}

contact.init();