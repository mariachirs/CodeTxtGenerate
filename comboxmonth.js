function renderMonthCombobox(selector, startMonth = 1) {
    // Array of month names in French
    const months = [
      "Janvier", "Février", "Mars", "Avril", "Mai", "Juin",
      "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre"
    ];

    // Ensure the startMonth is between 1 and 12
    startMonth = Math.max(1, Math.min(startMonth, 12));

    // Clear the existing options, in case the combobox already has some
    $(selector).empty();

    // Add an initial default option if needed
    $(selector).append('<option value="">Sélectionner un mois</option>');

    // Loop through the months starting from the specified startMonth
    for (let i = 0; i < months.length; i++) {
      // Calculate the index in a circular fashion using modulus operator
      const monthIndex = (startMonth - 1 + i) % 12;
      $(selector).append(`<option value="${monthIndex + 1}">${months[monthIndex]}</option>`);
    }
  }

  // On document ready, render the combobox starting from the current month
  $(document).ready(function() {
    renderMonthCombobox('#month-combobox');
  });
</script>
