    $('#gerer-versements-id #btnAnnulerGererVersementsCreation').click(() => {
        clearForm();
        uncheckErrors();
        utd.dialogue.masquer("gerer-versements-id");

    });

    function clearForm() {
        const fields = [
            '#id-versement',
            '#mode-paiement-id',
            '#entite-id',
            '#unite-administrative-id',
            '#compte-id',
            '#type-budget-id',
            '#numero-programme-id',
            '#psa-id',
            '#numero-projet-id'
        ];

        fields.forEach(field => {
            $(`#criteres-gestion-versements-modification ${field}`).val('');
        });
    }

    function uncheckErrors() {
        const errorFields = [
            '#champ-utd-mode-paiement',
            '#champ-utd-entite',
            '#champ-utd-unite-administrative',
            '#champ-utd-compte',
            '#champ-utd-type-budget',
            '#champ-utd-numero-programme',
            '#champ-utd-psa',
            '#champ-utd-numero-projet'
        ];

        $('.utd-avis-modifier-versement').hide().html('');

        errorFields.forEach(field => {
            $(`#criteres-gestion-versements-modification ${field}`).attr('invalide', false);
        });
    }
