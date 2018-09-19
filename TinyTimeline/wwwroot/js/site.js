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
    let confirmed = confirm("Delete event?");
    if (confirmed) {
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
}

function controlMergeButtonsVisibility() {
    let mergeButtons =  $('a[id^="mergeButton"]');
    
    if (mergeButtons.length > 1)
        mergeButtons.show();
    else 
        mergeButtons.hide();
}

function selectEvent(eventId, sessionId) {
    $('#' + eventId).addClass('selected_item');
    $('#buttons' + eventId).prepend(`<a class="btn btn-warning btn-xs" style="cursor: pointer" id="${'mergeButton' + eventId}">Merge</a>`);
    $('#mergeButton' + eventId).click(() => mergeEvents(sessionId));
    $('#select' + eventId).prop("onclick", null).off("click").text("Deselect").click(() => {deselectEvent(eventId, sessionId)});
    controlMergeButtonsVisibility()
}

function deselectEvent(eventId, sessionId) {
    $('#' + eventId).removeClass('selected_item');
    $('#select' + eventId).prop("onclick", null).off("click").text("Select").click(() => {selectEvent(eventId, sessionId)});
    $('#mergeButton' + eventId).remove();
    controlMergeButtonsVisibility()
}

function mergeEvents(sessionId) {
    let confirmed = confirm("Merge events?");
    if (confirmed) {
        let eventIds = $(".selected_item").toArray().map(x => x.id);
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
}

function deleteSession(sessionId) {
    let confirmed = confirm("Delete session?");
    if (confirmed) {
        $.ajax({
            type: 'DELETE',
            url: '/Interaction/DeleteSession',
            data: {
                sessionId: sessionId
            },
            success: function () {
                $("#" + sessionId).remove();
            }
        });
    }
}

function deleteReview(reviewId, sessionId) {
    let confirmed = confirm("Delete review?");
    if (confirmed) {
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
}

function skip(currentId) {
    $("#" + currentId).hide();
    checkIfDone();
}

function checkIfDone() {
    if ($('.vote_item').filter(":hidden" ).length == $('.vote_item').length)
        $("#voteEnd").show();
}

function initTextareaAutoresize() {
    $.each($('textarea[data-autoresize]'), function() {
        let offset = this.offsetHeight - this.clientHeight;

        let resizeTextarea = function (el) {
            $(el).css('height', 'auto').css('height', el.scrollHeight + offset);
        };

        resizeTextarea(this);
        $(this).on('keyup input', function() { resizeTextarea(this); }).removeAttr('data-autoresize');
    });
}