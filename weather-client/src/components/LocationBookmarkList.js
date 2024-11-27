import React from 'react';

const LocationBookmarkList = ({ bookmarks }) => {
  return (
    <div>
      <h3>Bookmarked Locations</h3>
      <table>
        <thead>
          <tr>
            <th>Location</th>
            <th>Country</th>
            <th>State</th>
          </tr>
        </thead>
        <tbody>
          {bookmarks.map((bookmark, index) => (
            <tr key={index}>
              <td>{bookmark.locationName}</td>
              <td>{bookmark.country}</td>
              <td>{bookmark.state}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default LocationBookmarkList;
