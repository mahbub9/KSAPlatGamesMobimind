var stripe={

    token:$('meta[name=csrf-token]').attr('content'),

    getUserByEmailPassword:function (data) {
        return $.ajax({headers: { 'X-CSRF-Token' : this.token }, url: '/checkUserStat',
            method:"POST", data:data , dataType: 'json'});
    },

    checkEmailFormat:function (email) {
        var pattern = /^\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b$/i;
        if(!pattern.test(email)){
            return false;
        }else {
            return true;
        }
    },


    openStripePopUp:function (email) {
        handler.open({
            name: 'GoGames',
            description: "NO ADS, NO WAITING",
            amount: 199,
            email:email
        });
    },

    handleStripePopupLogic : function (email, password) {
        stripe.getUserByEmailPassword({email:email,password:password}).done(function (response,textStatus, jqXHR) {

            if(response.status_code==200){
                stripe.openStripePopUp(email);
            }else if(response.status_code == 300){
                window.location.href = '/stripe/alreadySubscribed';
            }else {
                $('#authFailed').html(response.message);
                $('#authFailed').show();
                //$('#email').val('');
                $('#password').val('');
            }

        }).fail(function() {
            alert('Error Occured')
        });
    },


    checkEmail:function (event) {
        event.preventDefault();
        var email = $('#email').val();
        var password = $('#password').val();
        $('#errMsg').hide();
        $('#authFailed').hide();


        if(!stripe.checkEmailFormat(email)){
            $('#errMsg').show();
        }else {
            stripe.handleStripePopupLogic(email, password);
        }


    }
};

var handler = StripeCheckout.configure({
    key: 'pk_live_5JW4LjaRafnhjjdrV4YR6YY2',
    image: 'https://stripe.com/img/documentation/checkout/marketplace.png',
    token: function(token) {
        /* Use the token to create the charge with a server-side script.
         You can access the token ID with `token.id`
         Pass along various parameters you get from the token response
         and your form.*/
        var myData = {
            token: token.id,
            email: token.email,
            amount: 199,
            _token: stripe.token
        };
        /* Make an AJAX post request using JQuery,
         change the first parameter to your charge script*/
        $.post("/subscribe", myData,function (response) {
            // if you get some results back update results

            if(response.status_code==200){
                window.location.href = '/stripe/success';
            }else {
                window.location.href = '/stripe/failed';
            }
        }).fail(function () {
            window.location.href = '/stripe/failed';
        })
    }
});