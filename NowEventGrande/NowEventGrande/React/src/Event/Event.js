import '../styles/event.css';
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

function Event() {
    const [text, setText] = useState("");
    const [eventName, setEventName] = useState("");
    const navigate = useNavigate();
    const [isValid, setValid] = useState(true);
    const user = localStorage.getItem('user');
    const [userId, setUserId] = useState();
    console.log(user);
    const EventTypes = {
        Birthday: "Birthday",
        Festival:"Festival",
        Concert: "Concert"
    }

    useEffect(() => {
        fetchUserId();

    }, [])

    async function fetchUserId(){
        const res = await fetch('../account/GetCurrentUserId',{
            headers:{'Content-type':'application/json'},
            Authorization: !user ? {} : { 'Authorization': 'Bearer ' + user.accessToken }
          });
          if (!res.ok) {
            const message = `An error has occured: ${res.status} - ${res.statusText}`;
            throw new Error(message);
          }
          else{       
            await res.text()
            .then((result)=> { setUserId(result)}) 
            .catch(res)       
          }
    }
    
    function handleSubmit(evt) {
        evt.preventDefault();
        handlePost();
      }

    async function handlePost(){

        const res = await fetch('../events/CreateNewEvent',{
            method: 'POST',
            headers:{'Content-type':'application/json'},
            Authorization: !user ? {} : { 'Authorization': 'Bearer ' + user.accessToken },  
              body:  JSON.stringify({ClientId: userId, Type: text, Size: "Small", Name: eventName, Status: ""}) 
          });
          if (!res.ok) {
            setValid(false);
            const message = `An error has occured: ${res.status} - ${res.statusText}`;
            throw new Error(message);
          }
          else{       
            await res.json()
            .then((result)=> { console.log(result); navigate(`/event/${result}/main`, 
            {state: {eventId: result, eventName: eventName}});}) 
            .catch(res)       
          }
    }             
        
    return (       
            <div className="event">
                <h1>New Event</h1>
                <div>                 
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