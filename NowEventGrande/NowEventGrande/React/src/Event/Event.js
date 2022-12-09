import '../styles/event.css';
import { useState } from "react";
import { useNavigate } from "react-router-dom";

function Event() {
    const [text, setText] = useState("");
    const [eventName, setEventName] = useState("");
    const navigate = useNavigate();
    const [isValid, setValid] = useState(true);
    const user = localStorage.getItem('user');
    console.log(user);
    const EventTypes = {
        Birthday: "Birthday",
        Festival:"Festival",
        Concert: "Concert"
    }
    
    function handleSubmit(evt) {
        evt.preventDefault();
        handlePost();
      }

    async function handlePost(){
        console.log("here");

        const res = await fetch('../events/CreateNewEvent',{
            method: 'POST',
            headers:{'Content-type':'application/json'},
            Authorization: 'Bearer ' + user.accessToken,
            // headers: !token ? {} : { 'Authorization': `Bearer ${token}` },
              body:  JSON.stringify({ClientId: 1, Type: text, Size: "Small", Name: eventName, Status: ""}) 
          });
          if (!res.ok) {
            const message = `An error has occured: ${res.status} - ${res.statusText}`;
            setValid(false);
            
            throw new Error(message);
          }
          else{
            console.log("SOMETHING");
            console.log(res.status);
            
            await res.json().then((result)=> { console.log(result); navigate(`/event/${result}/main`, {state: {eventId: result, eventName: eventName}});})          
          }
    }             
        
    return (       
            <div className="event">
                <h1>New Event</h1>
                <div className="columns">                 
                    <div>                           
                        <br />
                        <h3>Choose your event category:</h3>  
                                       
                        <div className="row tileContainer">                            
                            <button className="Event-col-2 tile" onClick={e => setText(EventTypes.Birthday)}>
                                <h2 className="tileText">BIRTHDAY</h2>
                            </button>
                            <button className="Event-col-2 tile" onClick={e => setText(EventTypes.Festival)}>
                                <h2 className="tileText">FESTIVAL</h2>
                            </button>
                            <button className="Event-col-2 tile" onClick={e => setText(EventTypes.Concert)}>
                                <h2 className="tileText">CONCERT</h2>
                            </button>
                        </div>
                    </div>
                </div>
                <div className="createEventForm"> 
                    <h3 className="eventType">Create event: {text}</h3>                                                                       
                    <form  onSubmit={handleSubmit}>  
                    <label>Choose your event name:</label>
                    <input className="eventName" type="text" value={eventName} required onChange={e => setEventName(e.target.value)}/>                                                
                    <input className="addEventButton" id="createEvent" type="submit" value="Create" />
                    </form>
                </div>
                <div>
                    {!isValid && <p className="wrongInputMessage">Invalid information. Please keep in mind that you must choose event type. Special characters and numbers are not allowed in the title.</p>}
                </div>    
            </div>
    )
}

export default Event;