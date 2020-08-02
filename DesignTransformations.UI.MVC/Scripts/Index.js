$(document).ready(function () {
    /*
     * pasted for refernce purposes - tle 2020-08-02
                                   <div class="loading"> Loading</div>
                                <div class="error-message"> </div>
                                <div class="sent-message"> Your message has been sent. Thank you!</div>
     * */
    $('.loading').hide();
    $('.error-message').hide();
    $('.sent-message').hide();

    $("#sendMessage").click(function () {

        $('.loading').show();

        var contact_name = $('#name').val();
        var contact_email = $('#email').val();
        var subject = $('#subject').val();
        var body = $('#message').val();
        $.ajax({
            type: 'POST',
            url: "/Home/SendEmail/",
            //(string contact_email,string contact_name,string subject,string body)
            data: {'contact_email': contact_email, 'contact_name':contact_name, 'subject':subject, 'body': body},
            success: function (d,data) {
                $('.sent-message').show();
                $('.loading').hide();
            },
            error: function (response, error) {
                $('.error-message').append(error);
                $('.error-message').show();
                $('.loading').hide();
            }
        });
    });

});