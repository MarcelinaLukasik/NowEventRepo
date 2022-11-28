import '../styles/event.css';
import { useState } from "react";
import { useNavigate } from "react-router-dom";

function Event() {
    const [text, setText] = useState("");
    const [eventName, setEventName] = useState("");
    const navigate = useNavigate();

    const EventTypes = {
        Birthday: "Birthday",
        Festival:"Festival",
        Concert: "Concert"
    }
    
    function handleSubmit(evt) {
        evt.preventDefault();
        handlePost()
        .then(res=>{
            navigate(`/event/${res}/main`, {state: {eventId: res, eventName: eventName}});
              });
      }

    async function handlePost(){
        const response = await fetch('../events/CreateNewEvent',{
            method: 'POST',
            headers:{'Content-type':'application/json'},
              body:  JSON.stringify({ClientId: 1, Type: text, Size: "Small", Name: eventName, Status: ""}) 
          });
          const result = await response.json();
          return result;
    }             
        
    return (       
            <div className="event">
                <h1>New Event</h1>
                <div className="columns">                 
                    <div>                           
                        <br />
                        <h3>Choose your event category:</h3>  
                                       
                        <div className="row tileContainer">
                            {/* <div className="Event-col-4"></div> */}
                            
                            <button className="Event-col-2 tile" onClick={e => setText(EventTypes.Birthday)}>
                                <h2 className="tileText">BIRTHDAY</h2>
                            </button>
                            <button className="Event-col-2 tile" onClick={e => setText(EventTypes.Festival)}>
                                <h2 className="tileText">FESTIVAL</h2>
                            </button>
                            <button className="Event-col-2 tile" onClick={e => setText(EventTypes.Concert)}>
                                <h2 className="tileText">CONCERT</h2>
                            </button>
                            
                            {/* <div className="Event-col-4"></div> */}
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
            </div>
    )
}

export default Event;