/**
 * Formats a JSON date string into yyyy-MM-dd format.
 * @param {string} jsonDate - The JSON date string (e.g., "2024-11-19T12:34:56Z").
 * @returns {string} - The formatted date in yyyy-MM-dd format.
 */
function formatJsonDate(jsonDate) {
    if (!jsonDate) {
        throw new Error("Invalid date string");
    }
    const date = new Date(jsonDate); // Parse the date string
    if (isNaN(date.getTime())) {
        throw new Error("Invalid date format");
    }
    // Format as yyyy-MM-dd
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are 0-based
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
}
