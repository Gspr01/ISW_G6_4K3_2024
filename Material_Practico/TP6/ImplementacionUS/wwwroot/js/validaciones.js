document.addEventListener('DOMContentLoaded', function () {
        // Obtenemos los campos de fecha y el formulario
        const fechaRetiroInput = document.getElementById('fechaRetiro');
        const fechaEntregaInput = document.getElementById('fechaEntrega');
        const formulario = document.querySelector('form');

        // Añadimos un evento para cuando el formulario es enviado
        formulario.addEventListener('submit', function (event) {
            const fechaRetiro = new Date(fechaRetiroInput.value);
            const fechaEntrega = new Date(fechaEntregaInput.value);
            const fechaHoy = new Date();
            fechaRetiro.setDate(fechaRetiro.getDate()+1);
            fechaEntrega.setDate(fechaEntrega.getDate()+1);
            // Eliminar la parte de la hora en la fecha actual
            fechaHoy.setHours(0, 0, 0, 0);
            // Validación: la fecha de retiro debe ser mayor o igual a hoy
            if (fechaRetiro < fechaHoy) {
                event.preventDefault();
                // alert("La fecha de retiro no puede ser anterior a hoy.");
                mensaje="La fecha no puede ser anterior a hoy."
                alerta("retiro", mensaje);
                focus(fechaRetiroInput);
                return;
            }

            // Validación: la fecha de entrega debe ser mayor o igual a hoy y a la fecha de retiro
            if (fechaEntrega < fechaHoy || fechaEntrega < fechaRetiro) {
                event.preventDefault();
                // alert("La fecha de entrega no puede ser anterior a hoy ni a la fecha de retiro.");
                mensaje="La fecha no puede ser anterior a hoy ni a la fecha de retiro.";
                alerta("entrega", mensaje);
                return;
            }

            // Si todas las validaciones pasan, el formulario se enviará
        });
    });

    function alerta(input, texto){
        Swal.fire({
            title: "Error en la fecha de "+ input,
            text: texto,
            icon: "warning"
        });
    }