import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import "./CreateProduct.css";
import axios from 'axios';
import { apiUrl } from '../apiconfig';

const CreateEvent = () => {
  const navigate = useNavigate();

  const [eventData, setEventData] = useState({
    title: '',
    description: '',
    category: '',
    location: '',
    startDate: '',
    endDate: '',
    coverImage: null
  });

  const [errors, setErrors] = useState({
    title: '',
    description: '',
    category: '',
    location: '',
    startDate: '',
    endDate: '',
    coverImage: ''
  });

  useEffect(() => {
    console.log("localStorage.getItem",localStorage.getItem("editEventId"));
    let eventId = localStorage.getItem("editEventId");
    if (eventId !== ""&&eventId!==null) {
      editEvent();
    }
  }, []);

  async function editEvent() {
    const token = localStorage.getItem("token");

    try {
      let response = await axios.get(apiUrl + "/api/job/" + localStorage.getItem("editEventId"),
        { headers: { Authorization: `${token}` } }
      );

      console.log("response in id", response);
      response.data.startDate= new Date(response.data.startDate);
      response.data.endDate= new Date(response.data.endDate);
      response.data.startDate = response.data.startDate.toISOString().split('T')[0];
      response.data.endDate = response.data.endDate.toISOString().split('T')[0];
  
      setEventData(response.data);
    } catch (error) {
//      navigate("/error");
    }
  }

  const handleInputChange = (e) => {
    console.log(e.target.value,e.target.name);
    setEventData({ ...eventData, [e.target.name]: e.target.value });
    setErrors({ ...errors, [e.target.name]: '' });
  };

  const handleFileChange = (e) => {
    const file = e.target.files[0];
    if (file) {
      convertFileToBase64(file);
    }
  };

  const convertFileToBase64 = (file) => {
    const reader = new FileReader();
    reader.onloadend = () => {
      setEventData({ ...eventData, coverImage: reader.result });
    };
    reader.readAsDataURL(file);
  };

  const handleCreateEvent = async () => {
    const validationErrors = {};

    if (eventData.title === "") {
      validationErrors.title = "Title is required";
    }
    if (eventData.description === "") {
      validationErrors.description = "Description is required";
    }
    if (eventData.category === "") {
      validationErrors.category = "Category is required";
    }
    if (eventData.location === "") {
      validationErrors.location = "Location is required";
    }
    if (eventData.startDate === "") {
      validationErrors.startDate = "Start date is required";
    }
    if (eventData.endDate === "") {
      validationErrors.endDate = "End date is required";
    }
    if (eventData.coverImage === null) {
      validationErrors.coverImage = "Please select the Cover Image";
    }

    console.log("validationErrors", validationErrors);
    if (Object.keys(validationErrors).length > 0) {
      setErrors(validationErrors);
      return;
    }

    eventData.userId = JSON.parse(localStorage.getItem("userData")).userId;

    console.log("eventData", eventData);

    try {
      let eventId = localStorage.getItem("editEventId");
      if (eventId === "") {
        const token = localStorage.getItem("token");

        let createEventResponse = await axios.post(apiUrl + "/api/job", eventData,
          {
            headers: {
              'Content-Type': 'application/json',
              "Authorization": `${token}`
            }
          });

        if (createEventResponse.status === 200) {
          navigate("/userproducts");
        }
      } else {
        const token = localStorage.getItem("token");

        let updateEvent = await axios.put(apiUrl + "/api/job/" + localStorage.getItem("editEventId"), eventData,
          {
            headers: {
              'Content-Type': 'application/json',
              "Authorization": `${token}`
            }
          });

        if (updateEvent.status === 200) {
          navigate("/userproducts");
        }
      }
    } catch (error) {
//      navigate("/error");
    }
  };

  const categories = ['Teaching and Education', 'Technology and IT', 'Healthcare and Medical', 'Business and Finance', 'Other'];

  return (
    <div className="create-product-container">
      <button onClick={() => navigate(-1)}>Back</button>

      {localStorage.getItem("editEventId") === "" ? <h2>Create New Job</h2> : <h2>Update Job</h2>}

      <div className="form-group">
        <label>Title:</label>
        <input
          type="text"
          name="title"
          value={eventData.title}
          onChange={handleInputChange}
        />
        <span className="error-message">{errors.title}</span>
      </div>
      <div className="form-group">
        <label>Cover Image:</label>
        <input
          type="file"
          accept="image/*"
          onChange={handleFileChange}
        />
        <span className="error-message">{errors.coverImage}</span>
      </div>
      <div className="form-group">
        <label>Description:</label>
        <textarea
          name="description"
          value={eventData.description}
          onChange={handleInputChange}
        />
        <span className="error-message">{errors.description}</span>
      </div>

      <div className="form-group">
        <label>Category:</label>
        <select
          name="category"
          value={eventData.category}
          onChange={handleInputChange}
        >
          <option value="" disabled>Select category</option>
          {categories.map(category => (
            <option key={category} value={category}>
              {category}
            </option>
          ))}
        </select>
        <span className="error-message">{errors.category}</span>
      </div>

      <div className="form-group">
        <label>Location:</label>
        <input
          type="text"
          name="location"
          value={eventData.location}
          onChange={handleInputChange}
        />
        <span className="error-message">{errors.location}</span>
      </div>

      <div className="form-group">
        <label>Start Date:</label>
        <input
          type="date"
          name="startDate"
          value={eventData.startDate}
          onChange={handleInputChange}
        />
        <span className="error-message">{errors.startDate}</span>
      </div>

      <div className="form-group">
        <label>End Date:</label>
        <input
          type="date"
          name="endDate"
          value={eventData.endDate}
          onChange={handleInputChange}
        />
        <span className="error-message">{errors.endDate}</span>
      </div>

      <button className='submit-button' type="button" onClick={handleCreateEvent}>
        {localStorage.getItem("editEventId") === "" ? "Create Job" : "Update Job"}
      </button>
    </div>
  );
};

export default CreateEvent;
