import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "./UserProducts.css";
import axios from "axios";
import { apiUrl } from "../apiconfig.js";

const UserProducts = () => {
  const [showDeletePopup, setShowDeletePopup] = useState(false);
  const [showLogoutPopup, SetshowLogoutPopup] = useState(false);
  const [productToBeDelete, setProductToBeDelete] = useState(null);
  const [products, setProducts] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");
  const [sortValue, setSortValue] = useState(1);
  const navigate = useNavigate();
  const [selectedRental, setSelectedRental] = useState(null);
  const [showPopup, setShowPopup] = useState(false);

  useEffect(() => {
    localStorage.setItem("editEventId", "");
    console.log("came in grid");
    fun();
  }, [searchTerm, sortValue]);

  async function fun() {
    try {
      const token = localStorage.getItem("token");

      console.log("inside function");
const productResponse = await axios.get(
  apiUrl + `/api/job/user/${JSON.parse(localStorage.getItem("userData")).userId}?searchValue=${searchTerm}`  ,
  { 
    headers: { Authorization: `Bearer ${token}` },
  }
);
      console.log("productResponse", productResponse);
      if (productResponse.status === 200) {
        setProducts(productResponse.data);
      }
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

  const handleDeleteClick = (id) => {
    setProductToBeDelete(id);
    setShowDeletePopup(true);
  };

  async function deletefunction() {
    const productId = productToBeDelete;

    try {
      const token = localStorage.getItem("token");

      let deleteResponse = await axios.delete(
        apiUrl + `/api/job/${productId}`,
        { headers: { Authorization: `${token}` } }
      );
      if (deleteResponse.status === 200) {
        fun();
      }
      setShowDeletePopup(false);
    } catch (error) {
      console.log("error", error);
    }
  }

  return (
    <div>
      <div className={`ProductsList ${showDeletePopup||showLogoutPopup? "popup-open" : ""}`}>
        <button
          className="styledbutton"
          onClick={() => {
            // navigate("/login");
            SetshowLogoutPopup(true);
          }}
        >
          Logout
        </button>
        <button
          className="styledbutton"
          onClick={() => navigate("/createproduct")}
        >
          Add Job
        </button>
        <h1>My Jobs</h1>
        {/* Search functionality */}
        <input
          type="text"
          placeholder="Search by job title"
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
        />

        {/* Card layout */}
        <div className="card-container">
          {products.length ? (
            products.map((product) => (
              <div key={product._id} className="product-card"  >
                <img src={product.coverImage} alt={product.title} onClick={() => openPopup(product)}/>
                <div className="card-details">
                  <h2>{product.title}</h2>
                  {/* <p> {product.description}</p> */}
                  <p>Start Date: {new Date(product.startDate).toISOString().split('T')[0]}</p>
                  <div className="action-buttons">
                    <button
                    className="styledbutton"
                      style={{ backgroundColor: "red" }}
                      onClick={() => {
                        handleDeleteClick(product.jobId);
                      }}
                    >
                      Delete
                    </button>
                    <button
                                     className="styledbutton"

                      onClick={() => {
                        localStorage.setItem("editEventId", product.jobId);
                        navigate("/createproduct");
                      }}
                    >
                      Edit
                    </button>
                  </div>
                </div>
              </div>
            ))
          ) : (
            <div className="norecord">No records found</div>
          )}
        </div>
      </div>

      {showDeletePopup && (
        <div className="delete-popup">
          <p>Are you sure you want to delete?</p>
          <button onClick={deletefunction}>Yes, Delete</button>
          <button
            onClick={() => {
              setShowDeletePopup(false);
            }}
          >
            Cancel
          </button>
        </div>
      )}

{showLogoutPopup && (
        <div className="delete-popup">
          <p>Are you sure you want to Logout?</p>
          <button onClick={()=>{
            localStorage.clear();
            navigate("/login");
          }}>Yes, Logout</button>
          <button
            onClick={() => {
              SetshowLogoutPopup(false);
            }}
          >
            Cancel
          </button>
        </div>
      )}


{showPopup && selectedRental && (
                <div className="popup">
                    <div className="popup-content">
                        <span className="close" onClick={closePopup}>&times;</span>

                        <h2>{selectedRental.propertyName} Details</h2>
                        <p><strong>Description: </strong> {selectedRental.description}</p>
                        <p><strong>Location: </strong> {selectedRental.location}</p>
                        <p><strong>Start Date: </strong> { new Date(selectedRental.startDate).toISOString().split('T')[0]}</p>
                        <p><strong>End Date: </strong> { new Date(selectedRental.endDate).toISOString().split('T')[0]}</p>
                    </div>
                </div>
            )}
    </div>
  );
};

export default UserProducts;
