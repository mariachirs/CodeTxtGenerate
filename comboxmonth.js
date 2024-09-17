/ Function to render the combobox and start with the current month
  // Function to render the combobox with an option to start with the current month or January
  function renderMonthCombobox(selector, startFromCurrentMonth = false) {
    // Array of month names in French
    const months = [
      "Janvier", "Février", "Mars", "Avril", "Mai", "Juin",
      "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre"
    ];

    // Get the current month (JavaScript months are zero-based, so add 1)
    const currentMonth = new Date().getMonth() + 1; // Gives a number from 1 to 12

    // Clear the existing options, in case the combobox already has some
    $(selector).empty();

    // Add an initial default option
    $(selector).append('<option value="">Sélectionner un mois</option>');

    // If startFromCurrentMonth is true, loop starting from the current month
    if (startFromCurrentMonth) {
      for (let i = 0; i < months.length; i++) {
        // Calculate the index in a circular fashion using modulus operator
        const monthIndex = (currentMonth - 1 + i) % 12;
        $(selector).append(`<option value="${monthIndex + 1}">${months[monthIndex]}</option>`);
      }
    } else {
      // Otherwise, start from January and loop normally
      for (let i = 0; i < months.length; i++) {
        $(selector).append(`<option value="${i + 1}">${months[i]}</option>`);
      }
    }
  }

  // On document ready, render the combobox starting from the current month
  $(document).ready(function() {
    renderMonthCombobox('#month-combobox');
  });
</script>
