## Module One Features***
 1 Capture trainee participant information. This will be capture during capturing of admission information
 2 Once we capture that information. We will accept or reject admissions after searching for an admission by date range or name.
 3 Room allocation : For admitted students who come on training we should be able to assign rooms to them while they are on training

##The menu for the above features will therefore be
  1. Capture Admissions
  2. Manage Admissions (the form will search by date , name and status i.e rejected or accepted, beside each row there should be and action to accept or reject)
  3. Manage Students (this are students who have been admitted only. The page should have a search form at the top   to search by date admitted, name and student no.). Beside each row there should be actions to allocate room, deactivate student, assign transportation
  4. Room Allocation. Students will be allocated rooms on this page
  5. Manage Bookings. View Room allocations. Each roow will have actions to free booking
  6. Manage Rooms. This will show all rooms and their status(Occupied, Free/Empty, Partially occupied,) One should also be able to free rooms on this page
  
 ###Isssues so Far
  ##Admissions Page
  1. The buttons on the Manage Admissions Page are not intuitive. Please change them to Action dropdowns and Label them (Edit, View, Accept, Reject)
  2. Move the Print button toe th far right. Put export buttons for excel and csv
  
  ##Manage Students
  Move the print button to the far right. 
  The Search form must be properly arranged
  The buttons must be dropdown action buttons. They are too cluttered
  
  ##Manage Rooms
  Remove course of study from Room allocation form
  Evacuation Date does not appear bind the date when selected so the data does not reach the server
  Room setup page must have a field to upload picture so it will appear on managerooms page. Otherwise put a default picture there
  
  ##Header 
  Remove unneccessary icons from the header
  The Logged user's name should appear where David Nest name is
  Logo should be replaced with KAIPTC logo
    
  ##Dashboard Page
  The dashboard page must have the following data for now. Check the DashboardApiController for the endpoints
   1 Admissions(Grouped by :Accepted, Pending, Rejected)
   2 No of Students
   3 No of Available Rooms
   4 No of Allocated Rooms
  
  
  