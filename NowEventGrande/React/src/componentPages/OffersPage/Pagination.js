import React from 'react'
import ReactPaginate from "react-paginate";
const range = (start, end) => {
    return [...Array(end).keys()].map((el) => el + start);
}

export const Pagination = ({currentPage, totalPages, itemsFrom, itemsTo, setCurrentPage}) => {

    // console.log(currentPage)
    // console.log(totalPages)
    // console.log(itemsFrom)
    // console.log(itemsTo)
    const pages = range(1, totalPages)
  return (
    
    <div>Pagination<br/>
        <button
            disabled={currentPage === 1}
            onClick={() => setCurrentPage((prevState) => prevState - 1)}
        >
            Prev
        </button>
        <ul>
            <li>{range(1, 12)}</li>
        </ul>
        <p>{currentPage}</p>
        <button onClick={() => setCurrentPage((prevState) => prevState + 1)}>
        Next
        </button>
    </div>
    
  )
}
