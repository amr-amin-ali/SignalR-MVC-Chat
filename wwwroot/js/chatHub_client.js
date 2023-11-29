const GROUP_NAME = 'Your_Group_Here';
const JOHN_USER_ID = "afe9ea71-075b-41c1-a234-e3d18090b037";
const PUTIN_USER_ID = "be40ab9e-8a8a-43cc-a8b1-dea997cc7e2e";

//__ Create Connection
const connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();
/**
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */
//__Implement the method that the backend Hub will invoke
connection.on("ClientReceiver", (data) => {
    
    buildMessageCard('https://mdbcdn.b-cdn.net/img/Photos/Avatars/avatar-6.webp', data, true);
});


/*
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */
//- Send to all users including me
function SendToBackendToAllUsers() {
    connection.send("AllUsersSender", getMessage());
}

//- Send to all users but me
function SendToBackendToAllUsersButMe() {
    connection.send("AllUsersExceptSender", getMessage());
}

//- Send to the caller
function SendToBackendToTheCaller() {
    connection.invoke('SendMessageToCaller', getMessage()).catch(function (err) {
        console.error(err);
    });
}

//- Send to specific user bu ID
function SendToBackendToSpecificUser() {
    connection.invoke('SendMessageToSpecificUser', JOHN_USER_ID, getMessage()).catch(function (err) {
        console.error(err);
    });
}

//- Send to specific group of users
function SendToBackendToSpecificGroupOfUsers() {
    connection.invoke('SendMessageToGroup', GROUP_NAME, getMessage()).catch(function (err) {
        console.error(err);
    });
}

//- Send to all users except specific user
function SendToBackendToAllUsersExceptSpecificUser() {
    connection.invoke('SendMessageToAllExceptUser', PUTIN_USER_ID, getMessage()).catch(function (err) {
        console.error(err);
    });
}

//- Send ONE-to-ONE
function SendOneToSecondUser() {
    connection.invoke('SendMessageToRecipient', PUTIN_USER_ID, getMessage()).then(res => {
        //TO DO

    }).catch(function (err) {
        console.error(err);
    });
}
function SendOneToFirstUser() {
    connection.invoke('SendMessageToRecipient', JOHN_USER_ID, getMessage()).then(res => {
        //TO DO
    }).catch(function (err) {
        console.error(err);
    });
}

//- Join group
function joinGroup() {
    connection.invoke('JoinGroup', GROUP_NAME).catch(function (err) {
        console.error(err);
    });
}
//- leave group
function leaveGroup() {
    connection.invoke('LeaveGroup', GROUP_NAME).catch(function (err) {
        console.error(err);
    });
}

/*
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */


//__ Start the connection
connection.start().then(
    (_) => console.log("Connection Started."),
    (_) => console.log("Connection Failed.")
);




//-----  Utility
function buildMessageCard(imgageUrl, data, isLtr = true) {
    debugger
    const groupId = data.groupId;
    const fromId = data.fromId;
    const toId = data.toId;
    const message = data.message;
    // Create the <li> element
    var li = document.createElement('li');
    li.className = 'mb-4';

    // Create the outermost <div> element with class "row"
    var divRow = document.createElement('div');
    divRow.className = 'row';

    // Create the first <div> element with class "col-1"
    var divCol1 = document.createElement('div');
    divCol1.className = 'col-1';

    // Create the <img> element with src, alt, and class attributes
    var img = document.createElement('img');
    img.src = imgageUrl;
    img.alt = 'avatar';
    img.className = 'rounded-circle d-flex align-self-start me-3 shadow-1-strong';
    img.width = '60';



    // Create the second <div> element with class "col-11"
    var divCol11 = document.createElement('div');
    divCol11.className = 'col-11';

    // Create the <div> element with class "card"
    var divCard = document.createElement('div');
    divCard.className = 'card';

    // Create the <div> element with class "card-header d-flex justify-content-between p-3"
    var divCardHeader = document.createElement('div');
    divCardHeader.className = 'card-header d-flex justify-content-between p-3';

    // Create the <p> element with class "fw-bold mb-0" for the title
    var pTitle = document.createElement('p');
    pTitle.className = 'fw-bold mb-0';
    pTitle.textContent = groupId && groupId.length > 0 ? groupId:'public';

    // Create the <p> element with class "text-muted small mb-0" for the timestamp
    var pTimestamp = document.createElement('p');
    pTimestamp.className = 'text-muted small mb-0';
    pTimestamp.innerHTML = '<i class="far fa-clock"></i> 12 mins ago';

    // Append the title and timestamp <p> elements to the card header <div> element
    divCardHeader.appendChild(pTitle);
    divCardHeader.appendChild(pTimestamp);

    // Create the <div> element with class "card-body"
    var divCardBody = document.createElement('div');
    divCardBody.className = 'card-body';

    // Create the <p> element with class "mb-0" for the content
    var pContent = document.createElement('p');
    pContent.className = 'mb-0';
    //pContent.textContent = getMessage();
    pContent.textContent = message;

    // Append the content <p> element to the card body <div> element
    divCardBody.appendChild(pContent);

    // Append the card header and card body <div> elements to the card <div> element
    divCard.appendChild(divCardHeader);
    divCard.appendChild(divCardBody);
    divCol11.appendChild(divCard);
    divCol1.appendChild(img);
    // Append the col-1 and col-11 <div> elements to the row <div> element



    if (isLtr) {
        divRow.appendChild(divCol1);
        divRow.appendChild(divCol11);
    } else {
        divRow.appendChild(divCol11);
        divRow.appendChild(divCol1);
    }


    li.appendChild(divRow);

    // Append the <li> element to a parent container (e.g., <ul> or <ol>)
    var parentContainer = document.getElementById('messagesList'); // Replace 'parentContainer' with the actual ID of the parent container
    parentContainer.appendChild(li);
}


function getMessage() {
    return document.getElementById("textarea").value;
}
