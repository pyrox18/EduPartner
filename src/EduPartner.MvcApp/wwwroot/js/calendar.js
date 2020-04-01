var calendarApiEndpoint = $('#calendar').data('endpoint');

$('#calendar').fullCalendar({
    header: {
        left: 'title',
        center: '',
        right: 'today prev,next'
    },
    eventSources: [
        calendarApiEndpoint
    ]
});
