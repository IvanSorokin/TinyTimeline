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

function selectEvent(eventId, sessionId) {
    $('#' + eventId).addClass('picked_item');
    let mergeButton = 'mergeButton' + eventId;
    $('#buttons' + eventId).prepend(`<a class="btn btn-warning btn-xs" style="cursor: pointer" id="${mergeButton}">Merge</a>`);
    $('#mergeButton' + eventId).click(() => mergeEvents(sessionId));
    $('#select' + eventId).prop("onclick", null).off("click").text("Deselect").click(() => {deselectEvent(eventId, sessionId)});
}

function deselectEvent(eventId, sessionId) {
    $('#' + eventId).removeClass('picked_item');
    $('#select' + eventId).prop("onclick", null).off("click").text("Select").click(() => {selectEvent(eventId, sessionId)});
    $('#mergeButton' + eventId).remove();
}

function mergeEvents(sessionId) {
    let eventIds = $(".picked_item").toArray().map(x => x.id);
    $.ajax({
        type: 'POST',
        url: '/Interaction/MergeEvents',
        data: {
            eventIds: eventIds,
            sessionId: sessionId
        },
        success: function () {
            location.reload();
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