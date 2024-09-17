/ Function to render the combobox and start with the current month
  function renderMonthCombobox(selector) {
    // Array of month names in French
    const months = [
      "Janvier", "Février", "Mars", "Avril", "Mai", "Juin",
      "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre"
    ];

    // Get the current month (JavaScript months are zero-based, so add 1)
    const currentMonth = new Date().getMonth() + 1; // Gives a number from 1 to 12

    // Clear the existing options, in case the combobox already has some
    $(selector).empty();

    // Add an initial default option if needed
    $(selector).append('<option value="">Sélectionner un mois</option>');

    // Loop through the months starting from the current month
    for (let i = 0; i < months.length; i++) {
      // Calculate the index in a circular fashion using modulus operator
      const monthIndex = (currentMonth - 1 + i) % 12; // This will wrap around after December

      // Value is monthIndex + 1 since months are 1-based
      $(selector).append(`<option value="${monthIndex + 1}">${months[monthIndex]}</option>`);
    }
  }

  // On document ready, render the combobox starting from the current month
  $(document).ready(function() {
    renderMonthCombobox('#month-combobox');
  });
</script>
