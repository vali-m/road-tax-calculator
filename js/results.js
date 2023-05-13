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
            //routePathOutput: Microsoft.Maps.Directions.RoutePathOutput.routePoints,
            routeAttributes: 'routePath',
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

        var start = new Microsoft.Maps.Directions.Waypoint({
            location: new Microsoft.Maps.Location(45.7631222, 21.1052788)
        });
        var end = new Microsoft.Maps.Directions.Waypoint({
            location: new Microsoft.Maps.Location(45.7644241, 21.2996518)
        });
        directionsManager.addWaypoint(start);
        directionsManager.addWaypoint(end);
        
        // Add an event handler to the DirectionsManager
        Microsoft.Maps.Events.addHandler(directionsManager, 'directionsUpdated', function (ev) {
            console.log(ev);
            var routeLegs = ev.route[0].routeLegs;
            var streets = [];
            for (var i = 0; i < routeLegs.length; i++) {
                var itinerary = routeLegs[i].itineraryItems;
                for (var j = 0; j < itinerary.length; j++) {
                    var instruction = itinerary[j].preIntersectionHints[0];
                    console.log(instruction);
                    /*
                    var streetName = instruction.match(/on (.+?)\./)[1];
                    console.log(streetName);
                    */

                    streets.push({"name": instruction});
                }
            }
        });

        // Calculate the truck route and display it on the map
        directionsManager.calculateDirections();
    });

    $.ajax({
        url: "/api/CostCalculation/distance",
        method: "POST",
        data: {
            "streets": streets,
            "routeLengthInKm": 25.779,
            "weightInTons": 5
        },
        success: function(res) {
            console.log(res);
        },
        error: function(res) {
            console.log(res);
        }
    });
}