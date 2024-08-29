Simulate an AJAX Request
You can simulate an AJAX request to ensure that the ajaxStart listener is firing as expected.
javascript
Copiar código
$(document).ready(function() {
    // Attach ajaxStart listener
    $(document).ajaxStart(function() {
        console.log("ajaxStart listener is active and triggered.");
    });

    // Simulate an AJAX request to trigger the listener
    $.ajax({
        url: "your-server-endpoint",
        method: "GET"
    });
});
If the simulated AJAX request triggers the ajaxStart listener, it confirms that the listener is properly loaded.

4. Inspecting jQuery's Internal Data
To manually inspect if the listener is present, use your browser's developer tools to check jQuery's internal event data.
javascript
Copiar código
$(document).ready(function() {
    $(document).ajaxStart(function() {
        console.log("ajaxStart listener is active and triggered.");
    });

    // Inspect the event data
    console.log($._data($(document)[0], "events"));
});
