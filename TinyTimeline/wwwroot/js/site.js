function sendVote(eventId, sessionId, positive) {
    $.ajax({
            type: 'POST',
            url: '/Interaction/Vote',
            data: {
                isPositive: positive,
                eventId: eventId,
                sessionId: sessionId
            },
            success: function () {
                $("#" + eventId).hide();
                checkIfDone();
            }
        });
}

function toBeDiscussed(eventId, sessionId) {
    $.ajax({
        type: 'POST',
        url: '/Interaction/ToBeDiscussed',
        data: {
            eventId: eventId,
            sessionId: sessionId
        },
        success: function () {
            $("#" + eventId).hide();
            checkIfDone();
        }
    });
}

function saveConclusion(eventId, sessionId) {
    $.ajax({
        type: 'POST',
        url: '/Interaction/SaveConclusion',
        data: {
            eventId: eventId,
            sessionId: sessionId,
            conclusion: $("#conclusion" + eventId).val()
        },
        success: function () {
            alert("Saved")
        }
    });
}

function deleteEvent(eventId, sessionId) {
    $.ajax({
        type: 'DELETE',
        url: '/Interaction/DeleteEvent',
        data: {
            eventId: eventId,
            sessionId: sessionId
        },
        success: function () {
            $("#" + eventId).hide();
            checkIfDone();
        }
    });
}

function deleteReview(reviewId, sessionId) {
    $.ajax({
        type: 'DELETE',
        url: '/Interaction/DeleteReview',
        data: {
            reviewId: reviewId,
            sessionId: sessionId
        },
        success: function () {
            $("#" + reviewId).hide();
        }
    });
}

function skip(currentId) {
    $("#" + currentId).hide();
    checkIfDone();
}

function checkIfDone() {
    if ($('.vote_item').filter(":hidden" ).length == $('.vote_item').length)
        $("#voteEnd").show();
}