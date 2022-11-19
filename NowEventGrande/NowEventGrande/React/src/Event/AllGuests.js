import React, { useState, useEffect } from "react";
import { Trash } from "react-bootstrap-icons";
import Modal from 'react-bootstrap/Modal';


const AllGuests = ({eventId}) => {
  const [hasError, setErrors] = useState(false);
  const [guests, setState] = useState([]);
  const [removedGuestCount, setRemovedCount] = useState(0);
  const [id, setId] = useState("");
  const [show, setShow] = useState(false);
  const [value, setValue] = useState("Default");
  const handleClose = () => setShow(false);
  const currentEventId = eventId;

  //TODO change to post with choosen sort value
  useEffect(() => {
      async function fetchData() {
        const res = await fetch(`/events/${currentEventId}/all`);      
        res
          .json()
          .then(res => setState(res))
          .catch(err => setErrors(err))
      }
       fetchData();  
  }, []);

  useEffect(() => {
    async function fetchData() {
      const res = await fetch(`/events/${currentEventId}/all`);      
      res
        .json()
        .then(res => setState(res))
        .catch(err => setErrors(err))
    }
     fetchData();  
}, [removedGuestCount]);



  function removeGuest(evt)
  {
    evt.preventDefault();
    handlePost();
    handleClose();
  }

  function handleRemoveModal(event) {
    let id = event.currentTarget.id;
    setId(id);
    setShow(true); 
  }

  async function handleSortDescending(){
    const response = await fetch(`/events/${currentEventId}/all/descending`);
    const result = await response.json();
    setState(result);
  }

  async function handleSortAscending(){
    const response = await fetch(`/events/${currentEventId}/all/ascending`);
    const result = await response.json();
    setState(result);
  }

  function handleSortSubmit(evt) {
    evt.preventDefault();
    if (evt.target.value === "Descending"){
      setValue(evt.target.value);
      handleSortDescending();
    }  
    else if (evt.target.value === "Ascending"){
      setValue(evt.target.value);
      handleSortAscending();
    }  
    else {
      setValue(evt.target.value);
    }
  }

  async function handlePost(){ 
        async function fetchData() {
          const res = await fetch(`/events/removeGuest/${id}`, {
            method: "delete",
            headers: {
              "Content-Type": "application/json",
              "x-access-token": "token-value",
            },
            body: JSON.stringify({ Id: id }),
          });
          if (!res.ok) {
            const message = `An error has occured: ${res.status} - ${res.statusText}`;
            throw new Error(message);
          
          }
          const result = await res.json();
          setRemovedCount(removedGuestCount +1);
      }  
        fetchData();    
  }

  return (
    <div>
      <div className="selectSort">
        <label>Sort by:</label>
        <select className="sortGuests" value={value} onChange={handleSortSubmit}>
        <option value="Default">Default</option>
          <option value="Ascending">Ascending</option>
          <option value="Descending">Descending</option>         
        </select>     
      </div>
      <h2>Current guests on list:</h2>
          {Array.from(guests).map((item, i) => (
            <div className="guestInfo" key={i}>{item.quantityNeeded} {item.firstName + " " + item.lastName}
            <button className="removeButton" id={item.id} type="submit" value="Remove" 
            onClick={handleRemoveModal}><Trash color="white"/>
            </button>
            <hr className="solid"></hr>
            </div>                  
          ))}
          <Modal show={show} onHide={handleClose} className="removeModal">
            <Modal.Header >
              <Modal.Title>Remove confirmation</Modal.Title>
              </Modal.Header>
              <Modal.Body>Are you sure you want to remove this guest from list?</Modal.Body>
              <Modal.Footer>
                <container className="removeButtons">
                  <button className="rejectButton" variant="secondary" onClick={handleClose}>
                    No
                  </button>
                  <button className="acceptButton" variant="primary" onClick={removeGuest} >
                    Yes
                  </button>
                </container>             
            </Modal.Footer>
          </Modal>
        <h3>Total guests: {Array.from(guests).length} </h3>
        {/* <div>
          Has errors: {JSON.stringify(hasError)}
      </div>            */}
    </div>
  );      
};
export default AllGuests;