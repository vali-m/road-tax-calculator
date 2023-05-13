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
            location: new Microsoft.Maps.Location(45.7631222, 21.1052788)
        });
        var wp2 = new Microsoft.Maps.Directions.Waypoint({
            location: new Microsoft.Maps.Location(45.746409, 21.256399)
        });
        var wp3 = new Microsoft.Maps.Directions.Waypoint({
            location: new Microsoft.Maps.Location(45.7644241, 21.2996518)
        });
        directionsManager.addWaypoint(wp1);
        directionsManager.addWaypoint(wp2);
        directionsManager.addWaypoint(wp3);
        console.log(directionsManager);

        
        // Add an event handler to the DirectionsManager
        Microsoft.Maps.Events.addHandler(directionsManager, 'directionsUpdated', function (ev) {
            console.log(ev);
        });

        // Calculate the truck route and display it on the map
        directionsManager.calculateDirections({
            /*
            truckRoute: true,
            truckRouteOptions: truckRouteOptions,*/
            success: function(response) {
                console.log("test1");
                var route = response.route[0];
                console.log(route);
            },
            error: function(error) {
              console.log(error.message);
              console.log("err");
            }
        });
        
        console.log(directionsManager);
    });
}