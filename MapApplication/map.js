document.addEventListener('DOMContentLoaded', () => {
  const map = new ol.Map({
    target: 'map',
    layers: [
      new ol.layer.Tile({
        source: new ol.source.OSM()
      }),
      new ol.layer.Vector({
        source: new ol.source.Vector(),
        style: new ol.style.Style({
          image: new ol.style.Circle({
            radius: 5,
            fill: new ol.style.Fill({
              color: 'red'
            })
          })
        })
      })
    ],
    view: new ol.View({
      center: ol.proj.fromLonLat([35.2532, 39.5000]),
      zoom: 6.7
    })
  });

  let interaction = null;
  const panel = document.getElementById('panel');
  const queryPanel = document.getElementById('query-panel');
  const coordinatesList = document.getElementById('coordinates-list');
  let dataTable = null;
  const vectorSource = map.getLayers().getArray()[1].getSource();

  document.getElementById('add-point-btn').addEventListener('click', () => {
    if (interaction) {
      map.removeInteraction(interaction);
    }

    interaction = new ol.interaction.Select({
      layers: [map.getLayers().getArray()[1]]
    });

    map.addInteraction(interaction);
    map.on('singleclick', handleMapClick);
  });

  function handleMapClick(event) {
    const coordinate = event.coordinate;
    const lonLat = ol.proj.toLonLat(coordinate);

    panel.style.display = 'block';
    panel.style.left = `${event.pixel[0] + 10}px`;
    panel.style.top = `${event.pixel[1] + 10}px`;

    document.getElementById('pointx').value = lonLat[0];
    document.getElementById('pointy').value = lonLat[1];

    vectorSource.clear();
    const feature = new ol.Feature({
      geometry: new ol.geom.Point(coordinate)
    });
    vectorSource.addFeature(feature);

    document.getElementById('save-btn').addEventListener('click', () => {
      const xCoord = document.getElementById('pointx').value;
      const yCoord = document.getElementById('pointy').value;
      const name = document.getElementById('name').value;

      fetch('/api/point/addUOW', { // Endpoint updated
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({ xCoord, yCoord, name })
      })
      .then(response => response.json())
      .then(data => {
        console.log('Success:', data);
        panel.style.display = 'none';
        // Optionally refresh the list here
        document.getElementById('query-btn').click();
      })
      .catch(error => {
        console.error('Error:', error);
      });
    });

    panel.addEventListener('click', (e) => {
      if (e.target === panel) {
        panel.style.display = 'none';
      }
    });
  }

  document.getElementById('query-btn').addEventListener('click', () => {
    fetch('/api/point/getAllUOW') // Endpoint updated
      .then(response => response.json())
      .then(data => {
        if (data.length > 0) {
          const tbody = document.getElementById('coordinates-list');
          tbody.innerHTML = ''; // Clear previous data
          data.forEach(point => {
            const row = document.createElement('tr');
            row.innerHTML = `
              <td>${point.xCoord}</td>
              <td>${point.yCoord}</td>
              <td>${point.name}</td>
              <td>
                <button class="update-btn" data-id="${point.id}">Update</button>
                <button class="show-btn" data-id="${point.id}">Show</button>
                <button class="delete-btn" data-id="${point.id}">Delete</button>
              </td>
            `;
            tbody.appendChild(row);
          });

          if (dataTable) {
            dataTable.destroy(); // Destroy old table instance
          }

          dataTable = $('#coordinates-table').DataTable();

          document.querySelectorAll('.update-btn').forEach(btn => {
            btn.addEventListener('click', (e) => {
              const id = e.target.getAttribute('data-id');
              const updatedName = prompt('Enter new name:');
              if (updatedName) {
                fetch(`/api/point/updateUOW/${id}`, { // Endpoint updated
                  method: 'PUT',
                  headers: {
                    'Content-Type': 'application/json'
                  },
                  body: JSON.stringify({ name: updatedName })
                })
                .then(response => response.json())
                .then(data => {
                  console.log('Success:', data);
                  // Refresh the list after updating
                  document.getElementById('query-btn').click();
                })
                .catch(error => {
                  console.error('Error:', error);
                });
              }
            });
          });

          document.querySelectorAll('.show-btn').forEach(btn => {
            btn.addEventListener('click', (e) => {
              const id = e.target.getAttribute('data-id');
              fetch(`/api/point/getByIdUOW/${id}`) // Endpoint for showing details
                .then(response => response.json())
                .then(data => {
                  alert(`Point ID: ${data.id}\nX: ${data.xCoord}\nY: ${data.yCoord}\nName: ${data.name}`);
                })
                .catch(error => {
                  console.error('Error:', error);
                });
            });
          });

          document.querySelectorAll('.delete-btn').forEach(btn => {
            btn.addEventListener('click', (e) => {
              const id = e.target.getAttribute('data-id');
              fetch(`/api/point/deleteUOW/${id}`, { // Endpoint updated
                method: 'DELETE'
              })
              .then(response => response.json())
              .then(data => {
                console.log('Success:', data);
                // Refresh the list after deletion
                document.getElementById('query-btn').click();
              })
              .catch(error => {
                console.error('Error:', error);
              });
            });
          });

          queryPanel.style.display = 'block';
        } else {
          alert('No data available');
        }
      })
      .catch(error => {
        console.error('Error:', error);
      });
  });

  document.getElementById('close-query-btn').addEventListener('click', () => {
    queryPanel.style.display = 'none';
  });
});
