import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import "./RentalList.css";
import axios from 'axios';
import { apiUrl } from '../apiconfig';

const RentalList = () => {
    const [rentals, setRentals] = useState([]);
    const [selectedRental, setSelectedRental] = useState(null);
    const [showPopup, setShowPopup] = useState(false);
    const [searchTerm, setSearchTerm] = useState('');
    const [sortValue, setSortValue] = useState(1);
    const navigate = useNavigate();

    useEffect(() => {
        fetchRentals();
    }, [searchTerm, sortValue]);

    async function fetchRentals() {
        try {
            const userResponse = await axios.get(apiUrl + '/api/users', {
                headers: { Authorization: `${localStorage.getItem("token")}` }
            });
            const productResponse = await axios.get(
                `${apiUrl}/api/job?searchValue=${searchTerm}&sortValue=${sortValue}`,
                {
                    headers: { Authorization: `${localStorage.getItem("token")}` }
                }
            );
          console.log("userResponse",userResponse);

            const users = await userResponse.data;
            const rentalsData = await productResponse.data;

            const rentalsWithUserData = rentalsData.map((rental) => {
                const user = users.find(u => u._id === rental.owner);
                return {
                    ...rental,
                    userName: user ? `${user.firstName} ${user.lastName}` : "Unknown",
                    userEmail: user ? user.email : "Unknown",
                    userPhone: user ? user.mobileNumber : "Unknown"
                };
            });
console.log("rentalsWithUserData",rentalsWithUserData);
            setRentals(rentalsWithUserData);
        } catch (error) {
            // navigate("/error");
        }
    }

    const openPopup = (rental) => {
        setSelectedRental(rental);
        setShowPopup(true);
    };

    const closePopup = () => {
        setSelectedRental(null);
        setShowPopup(false);
    };

    return (
        <div className={`RentalList`}>
        <button className='styledbutton' onClick={() => { navigate("/login") }}>Logout</button>
        <h1>All Jobs</h1>
        <div className="filter-container">
                <div className="search-box">
                    <input
                        id='searchBox'
                        type="text"
                        placeholder="Search by job title"
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                    />
                </div>
                <div className="sort-box">
                    <label htmlFor="sortDropdown">Sort By Start Date:</label>
                    <select
                        id="sortDropdown"
                        value={sortValue}   
                        onChange={(e) => setSortValue(parseInt(e.target.value))}
                    >
                        <option value={1}>Date Ascending</option>
                        <option value={-1}>Date Descending</option>
                    </select>
                </div>
            </div>

        <div className="card-container">
            {rentals.length ? rentals.map((rental) => (
                <div key={rental._id} className="rental-card" onClick={() => openPopup(rental)}>
                    <img src={rental.coverImage} alt={rental.coverImage} />
                    <div className="card-details">
                        <h2>{rental.title}</h2>
                        <p>{rental.location}</p>
                        {/* <p>Description: {rental.description}</p> */}
                        <p>Start date: {new Date(rental.startDate).toISOString().split('T')[0]}</p>
                    </div>
                </div>
            )) : (
                <div className="norecord" style={{ textAlign: "center" }}>
                    No records found
                </div>
            )}
        </div>

            {showPopup && selectedRental && (
                <div className="popup">
                    <div className="popup-content">
                        <span className="close" onClick={closePopup}>&times;</span>

                        <h2>{selectedRental.propertyName} Details</h2>
                        <p><strong>Description: </strong> {selectedRental.description}</p>
                        <p><strong>Location: </strong> {selectedRental.location}</p>
                        <p><strong>Start Date: </strong> { new Date(selectedRental.startDate).toISOString().split('T')[0]}</p>
                        <p><strong>End Date: </strong> { new Date(selectedRental.endDate).toISOString().split('T')[0]}</p>
                        <p><strong>Posted By: </strong> {selectedRental.userName}</p>
                        <p><strong>Contact Email: </strong> {selectedRental.userEmail}</p>
                        <p><strong>Contact Phone: </strong> {selectedRental.userPhone}</p>
                    </div>
                </div>
            )}
        </div>
    );
};

export default RentalList;
