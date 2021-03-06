export function createGuid(version) {  
    switch (version) {
       case 'N' :
          return 'xxxxxxxxxxxxxxxxyxxxxxxxxxxxxxxx'.replace(/[xy]/g, function(c) {  
             // eslint-disable-next-line no-mixed-operators
             var r = Math.random()*16|0, v = c === 'x' ? r : (r&0x3|0x8);  
             return v.toString(16);
          });
       default :
          return 'xxxxxxxx-xxxx-xxxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {  
             // eslint-disable-next-line no-mixed-operators
             var r = Math.random()*16|0, v = c === 'x' ? r : (r&0x3|0x8);  
             return v.toString(16);
          });
    }
 }