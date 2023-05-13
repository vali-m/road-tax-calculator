function loadMapScenario() {
    var map = new Microsoft.Maps.Map(document.getElementById('myMap'), {
        /* No need to set credentials if already passed in URL */
        center: new Microsoft.Maps.Location(45.750046, 21.229915),
        zoom: 13.3
    });
    Microsoft.Maps.loadModule('Microsoft.Maps.Directions', function () {
        var directionsManager = new Microsoft.Maps.Directions.DirectionsManager(map);
        //directionsManager.setRenderOptions({ itineraryContainer: document.getElementById('printoutPanel') });
        directionsManager.setRequestOptions({
            routeMode: Microsoft.Maps.Directions.RouteMode.truck,
            vehicleSpec: {
                dimensionUnit: 'm',
                weightUnit: 'kg',
                vehicleHeight: 5,
                vehicleWidth: 3.5,
                vehicleLength: 30,
                vehicleWeight: 5000,
                vehicleAxles: 3,
                vehicleTrailers: 2,
                vehicleSemi: true,
                vehicleMaxGradient: 10,
                vehicleMinTurnRadius: 15,
                vehicleAvoidCrossWind: true,
                vehicleAvoidGroundingRisk: true,
                vehicleHazardousMaterials: 'F',
                vehicleHazardousPermits: 'F'
            }
        });
        var wp1 = new Microsoft.Maps.Directions.Waypoint({
            address: '590 Crane Ave, Pittsburgh, PA',
            location: new Microsoft.Maps.Location(45.7631222, 21.1052788)
        });
        var wp2 = new Microsoft.Maps.Directions.Waypoint({
            address: '600 Forbes Ave, Pittsburgh, PA',
            location: new Microsoft.Maps.Location(45.7644241, 21.2996518)
        });
        directionsManager.addWaypoint(wp1);
        directionsManager.addWaypoint(wp2);
        directionsManager.calculateDirections({
            success: function(response) {
              var jsonResponse = response.route[0].routeLegs[0].itineraryItems;
              console.log(response);
              console.log(jsonResponse);
              console.log("test");
            },
            error: function(error) {
              console.log(error);
            }
          });
    });
    
}
