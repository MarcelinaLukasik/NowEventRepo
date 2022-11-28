import SideBar from "./SideBar";
import React from 'react';
import {useLocation} from 'react-router-dom';
import {Col} from "react-bootstrap";
import calendarIcon from '../images/icons/salary.png';
import { useState, useEffect, setState, setErrors } from "react";
import '../styles/event.css';
import {handleStyle} from "./HandleProgress";


function Budget() {
    const location = useLocation();
    const eventId = location.state.EventId;
    const [rent, setRent] = useState("");
    const [decoration, setDecoration] = useState("");
    const [food, setFood] = useState("");
    const [budget, setBudget] = useState([]);
    const [count, setCount] = useState(0);
    const [isValid, setValid] = useState(true);

    const [isRentOpen, setRentOpen] = useState(false);
    const openRentInput = () => {setRentOpen(!isRentOpen);}
    const [isDecorOpen, setDecorOpen] = useState(false);
    const openDecorInput = () => {setDecorOpen(!isDecorOpen);}
    const [isFoodOpen, setFoodOpen] = useState(false);
    const openFoodInput = () => {setFoodOpen(!isFoodOpen);}
   

    useEffect(() =>{ 
        fetchStatsData();  
        fetchProgress();
    }, [])

    useEffect(() => {
      handleStyle(count);
    }, [count]);

    async function fetchProgress() {
      const res = await fetch(`/events/${eventId}/GetChecklistProgress`);      
      res
        .json()
        .then(res => setCount(res));
    }

    async function handleSubmit(evt) {
        evt.preventDefault();
        const costToChange = evt.target.id;
        if (costToChange === "rent")
        {
            handlePatch("RentPrice", rent).then(() => {
              fetchStatsData().then(() => {openRentInput()}).then(() => {fetchProgress()});
        })
        }
        else if (costToChange === "decoration")
        {
          await handlePatch("DecorationPrice", decoration).then(() => {
            fetchStatsData().then(() => {openDecorInput()}).then(() => {fetchProgress()});
        })
        }
        else if (costToChange === "food")
        {
          await handlePatch("FoodPrice", food).then(() => {
            fetchStatsData().then(() => {openFoodInput()}).then(() => {fetchProgress()});
        })
        }
  }

    async function fetchStatsData() {
      const res = await fetch(`/budget/${eventId}/GetBudgetStats`);
      res
        .json()
        .then(res => setBudget(res))
        .then()
        .catch(err => setErrors(err));
      }

    async function handlePatch(typeToChange, valueToChange) {
        const res = await fetch(`/budget/${eventId}/Update/${typeToChange}`, {
          method: "PATCH",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(valueToChange) ,
        });
        if (!res.ok) {
          const message = `An error has occured: ${res.status} - ${res.statusText}`;
          setValid(false);
          throw new Error(message);
        }
        else{
          setValid(true);
        }
        const result = await res;
        return result;
      }


    return (
        <div className="event">          
            {/* <h3 className="eventTitle">
            Event id: {eventId}
            </h3>    */}
            <div className="row">
                    <div className="Event-col-12">    
                        <div className="progressBarContainer"> 
                            <h3 className="progressText" >Checklist progress:</h3>  
                                <div className="progress" id="progress">
                                    <div className="progress-bar" id="progress-bar"></div>
                                </div> 
                        </div>
                    </div>
                </div>   
            <h1>Budget</h1>
            <div className="row">
                    <div className="Event-col-3">
                    <div className="sidebar">
                      <SideBar eventId={eventId}/>
                    </div>
                    </div>
                   <div className="Event-col-5">              
                        <br/>                                                               
                        <form onSubmit={handleSubmit} className="budgetForm" id="rent">
                            <div>                           
                            {!isRentOpen &&
                            <div>
                            <label>Rental cost: </label>
                            <label>{budget.rentPrice}$</label>
                            <input className="editCost"  type="button" value="Edit" onClick={openRentInput} isOpen={isRentOpen}/> 
                            </div>   
                            }
                            {isRentOpen &&
                            <div >
                            <label>Rental cost: </label>
                            <input className="costInput" type="text" value={rent} required onChange={e => setRent(e.target.value)}/>
                            <input className="saveCosts"  type="submit" value="Save"/>
                            </div>
                            }
                            </div>                                                                         
                        </form>  

                        <form onSubmit={handleSubmit} className="budgetForm" id="decoration">
                            <div>                           
                            {!isDecorOpen &&
                            <div>
                            <label>Decoration cost: </label>
                            <label>{budget.decorationPrice}$</label>
                            <input className="editCost"  type="button" value="Edit" onClick={openDecorInput} isOpen={isDecorOpen}/> 
                            </div>   
                            }
                            {isDecorOpen &&
                            <div >
                            <label>Decoration cost: </label>
                            <input className="costInput" type="text" value={decoration} required onChange={e => setDecoration(e.target.value)}/>
                            <input className="saveCosts"  type="submit" value="Save"/>
                            </div>
                            }
                            </div>                                                                         
                        </form>  

                        <form onSubmit={handleSubmit} className="budgetForm" id="food">
                            <div>                           
                            {!isFoodOpen &&
                            <div>
                            <label>Food cost: </label>
                            <label>{budget.foodPrice}$</label>
                            <input className="editCost"  type="button" value="Edit" onClick={openFoodInput} isOpen={isFoodOpen}/> 
                            </div>   
                            }
                            {isFoodOpen &&
                            <div >
                            <label>Food cost: </label>
                            <input className="costInput" type="text" value={food} required onChange={e => setFood(e.target.value)}/>
                            <input className="saveCosts"  type="submit" value="Save"/>
                            </div>
                            }
                            </div>                                                                         
                        </form>  
                        <div>
                           {!isValid && <p className="wrongInputMessage">Please provide valid numbers!</p>}
                        </div>     
                   </div>
                   <div className="Event-col-2">
                      <h3>Total cost: {budget.total}</h3>
                   </div>  
                   <div className="Event-col-2">
                    <Col cs={12} md={6} xl={6}>
                        <img src={calendarIcon} alt="img" className="featureIcon"/>
                    </Col>
                    </div>                    
                </div>        
        </div>
)
}

export default Budget;